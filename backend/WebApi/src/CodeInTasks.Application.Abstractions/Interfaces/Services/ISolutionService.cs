using CodeInTasks.WebApi.Models.Solution;

namespace CodeInTasks.Application.Abstractions.Interfaces.Services
{
    public interface ISolutionService
    {
        Task<Guid> AddAsync(SolutionCreateModel solutionCreateModel);
        Task UpdateStatusAsync(SolutionStatusUpdateModel statusUpdateModel);

        Task<Solution> GetAsync(Guid solutionId);
        Task<IEnumerable<Solution>> GetFilteredAsync(SolutionFilterModel filterModel);
    }
}
