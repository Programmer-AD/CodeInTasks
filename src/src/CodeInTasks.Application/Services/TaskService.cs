using AutoMapper;
using CodeInTasks.Application.Abstractions.Dtos.Task;

namespace CodeInTasks.Application.Services
{
    internal class TaskService : ITaskService
    {
        private readonly IRepository<TaskModel> taskRepository;
        private readonly IFiltrationPipeline<TaskFilterDto, TaskModel> filtrationPipeline;
        private readonly IMapper mapper;

        public TaskService(
            IRepository<TaskModel> taskRepository,
            IFiltrationPipeline<TaskFilterDto, TaskModel> filtrationPipeline,
            IMapper mapper)
        {
            this.taskRepository = taskRepository;
            this.filtrationPipeline = filtrationPipeline;
            this.mapper = mapper;
        }

        public Task<Guid> AddAsync(TaskCreateDto taskCreateDto)
        {
            var taskModel = mapper.Map<TaskModel>(taskCreateDto);

            var resultTask = taskRepository.AddAsync(taskModel);

            return resultTask;
        }

        public async Task DeleteAsync(Guid taskId)
        {
            var isDeleted = await taskRepository.DeleteAsync(taskId);

            if (!isDeleted)
            {
                throw new EntityNotFoundException(nameof(TaskModel), taskId);
            }
        }

        public Task<IEnumerable<TaskModel>> GetFilteredAsync(TaskFilterDto filterDto)
        {
            var pipelineResult = filtrationPipeline.GetResult(filterDto);
            var filter = new RepositoryFilter<TaskModel>()
            {
                FiltrationPredicate = pipelineResult.FilterExpression,
                OrderFunction = pipelineResult.OrderFunction,
                Take = filterDto.TakeCount,
                Skip = filterDto.TakeOffset
            };

            var resultTask = taskRepository.GetFilteredAsync(filter);

            return resultTask;
        }

        public Task<TaskModel> GetAsync(Guid taskId)
        {
            var resultTask = GetTaskAsync(taskId);

            return resultTask;
        }

        public async Task UpdateAsync(TaskUpdateDto taskUpdateDto)
        {
            var taskId = taskUpdateDto.Id;
            var taskModel = await GetTaskAsync(taskId);

            mapper.Map(taskUpdateDto, taskModel);

            await taskRepository.UpdateAsync(taskModel);
        }

        public Task<bool> IsOwnerAsync(Guid taskId, Guid userId)
        {
            var resultTask = taskRepository.AnyAsync(x => x.Id == taskId && x.CreatorId == userId);

            return resultTask;
        }

        private async Task<TaskModel> GetTaskAsync(Guid taskId)
        {
            var taskModel = await taskRepository.GetAsync(taskId);

            return taskModel ?? throw new EntityNotFoundException(nameof(TaskModel), taskId);
        }
    }
}
