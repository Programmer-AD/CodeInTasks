using CodeInTasks.Application.Abstractions.Dtos.Solution;

namespace CodeInTasks.Application.Abstractions.Interfaces.Services
{
    public interface ISolutionService
    {
        Task<Guid> AddAsync(SolutionCreateDto solutionCreateDto);
        Task UpdateStatusAsync(SolutionStatusUpdateDto statusUpdateDto);

        Task<Solution> GetAsync(Guid solutionId);
        Task<IEnumerable<Solution>> GetFilteredAsync(SolutionFilterDto filterDto);
    }
}
