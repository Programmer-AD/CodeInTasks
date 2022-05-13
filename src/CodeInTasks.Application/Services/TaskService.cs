using AutoMapper;
using CodeInTasks.Application.Dtos.Task;

namespace CodeInTasks.Application.Services
{
    internal class TaskService : ITaskService
    {
        private readonly IRepository<TaskModel> taskRepository;
        private readonly IMapper mapper;


        public TaskService(IRepository<TaskModel> taskRepository, IMapper mapper)
        {
            this.taskRepository = taskRepository;
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

        public Task<IEnumerable<TaskViewDto>> GetAllAsync(TaskFilterDto filterDto)
        {
            //TODO: TaskService.GetAllAsync
            throw new NotImplementedException();
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
