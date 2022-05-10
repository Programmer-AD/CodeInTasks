using CodeInTasks.Application.Dtos.Task;

namespace CodeInTasks.Application.Interfaces.Services
{
    public interface ITaskService
    {
        Task<Guid> AddAsync(TaskCreateDto taskModel);
        Task UpdateAsync(TaskCreateDto taskModel);
        Task DeleteAsync(Guid id);

        Task<TaskViewDto> GetAsync(Guid id);
        Task<IEnumerable<TaskViewDto>> GetAllAsync(TaskFilterDto filterDto);
    }
}
