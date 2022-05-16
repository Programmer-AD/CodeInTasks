using AutoMapper;
using CodeInTasks.Application.Dtos.Task;

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

        public async Task<Guid> AddAsync(TaskCreateDto taskCreateDto)
        {
            var taskModel = mapper.Map<TaskModel>(taskCreateDto);

            var taskId = await taskRepository.AddAsync(taskModel);

            return taskId;
        }

        public async Task DeleteAsync(Guid taskId)
        {
            var isDeleted = await taskRepository.DeleteAsync(taskId);
            
            if (!isDeleted)
            {
                throw new EntityNotFoundException($"Not found task with id \"{taskId}\"");
            }
        }

        public async Task<IEnumerable<TaskViewDto>> GetFilteredAsync(TaskFilterDto filterDto)
        {
            var pipelineResult = filtrationPipeline.GetResult(filterDto);

            var taskModels = await taskRepository.GetAllAsync(
                pipelineResult.FilterExpression,
                pipelineResult.OrderFunction,
                filterDto.TakeCount,
                filterDto.TakeOffset);

            var taskViewDtos = mapper.Map<IEnumerable<TaskViewDto>>(taskModels);

            return taskViewDtos;
        }

        public async Task<TaskViewDto> GetAsync(Guid taskId)
        {
            var taskModel = await GetTaskAsync(taskId);

            var taskViewDto = mapper.Map<TaskViewDto>(taskModel);

            return taskViewDto;
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

            if (taskModel != null)
            {
                throw new EntityNotFoundException($"Not found task with id \"{taskId}\"");
            }

            return taskModel;
        }
    }
}
