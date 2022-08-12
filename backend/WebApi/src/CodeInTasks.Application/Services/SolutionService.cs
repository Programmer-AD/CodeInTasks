using AutoMapper;
using CodeInTasks.WebApi.Models.Solution;

namespace CodeInTasks.Application.Services
{
    internal class SolutionService : ISolutionService
    {
        private readonly IRepository<Solution> solutionRepository;
        private readonly ISolutionCheckEnqueuer checkEnqueuer;
        private readonly IFiltrationPipeline<SolutionFilterModel, Solution> filtrationPipeline;
        private readonly IMapper mapper;

        public SolutionService(
            IRepository<Solution> solutionRepository,
            ISolutionCheckEnqueuer checkEnqueuer,
            IFiltrationPipeline<SolutionFilterModel, Solution> filtrationPipeline,
            IMapper mapper)
        {
            this.solutionRepository = solutionRepository;
            this.checkEnqueuer = checkEnqueuer;
            this.filtrationPipeline = filtrationPipeline;
            this.mapper = mapper;
        }

        public async Task<SolutionCreateResultModel> AddAsync(SolutionCreateModel solutionCreateModel)
        {
            var solution = mapper.Map<Solution>(solutionCreateModel);
            await AssertSolutionNotQueuedAsync(solution);

            solution.Id = await solutionRepository.AddAsync(solution);

            await checkEnqueuer.EnqueueSolutionCheck(solution);

            var result = new SolutionCreateResultModel()
            {
                SolutionId = solution.Id
            };

            return result;
        }

        public Task<IEnumerable<Solution>> GetFilteredAsync(SolutionFilterModel filterModel)
        {
            var pipelineResult = filtrationPipeline.GetResult(filterModel);
            var filter = new RepositoryFilter<Solution>()
            {
                FiltrationPredicate = pipelineResult.FilterExpression,
                OrderFunction = pipelineResult.OrderFunction,
                Take = filterModel.TakeCount,
                Skip = filterModel.TakeOffset
            };

            var resultTask = solutionRepository.GetFilteredAsync(filter);

            return resultTask;
        }

        public Task<Solution> GetAsync(Guid solutionId)
        {
            var resultTask = GetSolutionAsync(solutionId);

            return resultTask;
        }

        public async Task UpdateStatusAsync(SolutionStatusUpdateModel statusUpdateModel)
        {
            var solutionId = statusUpdateModel.Id;
            var solution = await GetSolutionAsync(solutionId);

            mapper.Map(statusUpdateModel, solution);

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
