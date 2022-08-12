using AutoMapper;
using CodeInTasks.WebApi.Models.Task;

namespace CodeInTasks.Application.Services
{
    internal class TaskService : ITaskService
    {
        private readonly IRepository<TaskModel> taskRepository;
        private readonly IFiltrationPipeline<TaskFilterModel, TaskModel> filtrationPipeline;
        private readonly IMapper mapper;

        public TaskService(
            IRepository<TaskModel> taskRepository,
            IFiltrationPipeline<TaskFilterModel, TaskModel> filtrationPipeline,
            IMapper mapper)
        {
            this.taskRepository = taskRepository;
            this.filtrationPipeline = filtrationPipeline;
            this.mapper = mapper;
        }

        public Task<TaskCreateResultModel> AddAsync(TaskCreateModel taskCreateModel)
        {
            var taskModel = mapper.Map<TaskModel>(taskCreateModel);

            var resultTask = taskRepository.AddAsync(taskModel)
                .ContinueWith(addTask =>
                {
                    var taskId = addTask.Result;

                    return new TaskCreateResultModel()
                    {
                        TaskId = taskId,
                    };
                });

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

        public Task<IEnumerable<TaskModel>> GetFilteredAsync(TaskFilterModel filterModel)
        {
            var pipelineResult = filtrationPipeline.GetResult(filterModel);
            var filter = new RepositoryFilter<TaskModel>()
            {
                FiltrationPredicate = pipelineResult.FilterExpression,
                OrderFunction = pipelineResult.OrderFunction,
                Take = filterModel.TakeCount,
                Skip = filterModel.TakeOffset
            };

            var resultTask = taskRepository.GetFilteredAsync(filter);

            return resultTask;
        }

        public Task<TaskModel> GetAsync(Guid taskId)
        {
            var resultTask = GetTaskAsync(taskId);

            return resultTask;
        }

        public async Task UpdateAsync(TaskUpdateModel taskUpdateModel)
        {
            var taskId = taskUpdateModel.TaskId;
            var taskModel = await GetTaskAsync(taskId);

            mapper.Map(taskUpdateModel, taskModel);

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
