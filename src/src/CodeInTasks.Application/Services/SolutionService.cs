using AutoMapper;
using CodeInTasks.Application.Abstractions.Dtos.Solution;

namespace CodeInTasks.Application.Services
{
    internal class SolutionService : ISolutionService
    {
        private readonly IRepository<Solution> solutionRepository;
        private readonly ISolutionCheckQueue checkQueue;
        private readonly IFiltrationPipeline<SolutionFilterDto, Solution> filtrationPipeline;
        private readonly IMapper mapper;

        public SolutionService(
            IRepository<Solution> solutionRepository,
            ISolutionCheckQueue checkQueue,
            IFiltrationPipeline<SolutionFilterDto, Solution> filtrationPipeline,
            IMapper mapper)
        {
            this.solutionRepository = solutionRepository;
            this.checkQueue = checkQueue;
            this.filtrationPipeline = filtrationPipeline;
            this.mapper = mapper;
        }

        public async Task<Guid> AddAsync(SolutionCreateDto solutionCreateDto)
        {
            var solution = mapper.Map<Solution>(solutionCreateDto);
            await AssertSolutionNotQueuedAsync(solution);

            var solutionId = await solutionRepository.AddAsync(solution);

            solution.Id = solutionId;
            var queueDto = mapper.Map<SolutionQueueDto>(solution);
            await checkQueue.EnqueueSolutionCheck(queueDto);

            return solutionId;
        }

        public Task<IEnumerable<Solution>> GetFilteredAsync(SolutionFilterDto filterDto)
        {
            var pipelineResult = filtrationPipeline.GetResult(filterDto);
            var filter = new RepositoryFilter<Solution>()
            {
                FiltrationPredicate = pipelineResult.FilterExpression,
                OrderFunction = pipelineResult.OrderFunction,
                Take = filterDto.TakeCount,
                Skip = filterDto.TakeOffset
            };

            var resultTask = solutionRepository.GetFilteredAsync(filter);

            return resultTask;
        }

        public Task<Solution> GetAsync(Guid solutionId)
        {
            var resultTask = GetSolutionAsync(solutionId);

            return resultTask;
        }

        public async Task UpdateStatusAsync(SolutionStatusUpdateDto statusUpdateDto)
        {
            var solutionId = statusUpdateDto.Id;
            var solution = await GetSolutionAsync(solutionId);

            mapper.Map(statusUpdateDto, solution);

            await solutionRepository.UpdateAsync(solution);
        }

        private async Task<Solution> GetSolutionAsync(Guid solutionId)
        {
            var solution = await solutionRepository.GetAsync(solutionId);

            return solution ?? throw new EntityNotFoundException(nameof(Solution), solutionId);
        }

        private async Task AssertSolutionNotQueuedAsync(Solution solution)
        {
            var isAlreadyQueued = await solutionRepository.AnyAsync(
                x => x.TaskId == solution.TaskId
                    && x.SenderId == solution.SenderId
                    && x.Status != TaskSolutionStatus.Finished);

            if (isAlreadyQueued)
            {
                throw new SolutionAlreadyQueuedException(solution);
            }
        }
    }
}
