namespace CodeInTasks.Application.Interfaces.Services
{
    public interface ITaskService
    {
        Task<Guid> AddAsync(TaskModel taskModel);
        Task UpdateAsync(TaskModel taskModel);
        Task DeleteAsync(Guid id);

        Task<TaskModel> GetAsync(Guid id);

        //TODO: GetAll with filtering variations
    }
}
