namespace CodeInTasks.Application.Interfaces.Services
{
    public interface ISolutionService
    {
        Task<Guid> AddAsync(TaskSolution solution);
        Task<TaskSolution> GetAsync(Guid id);

        //TODO: GetAll with filtration
    }
}
