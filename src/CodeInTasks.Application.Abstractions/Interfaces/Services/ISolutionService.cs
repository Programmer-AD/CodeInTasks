using CodeInTasks.Application.Abstractions.Dtos.Solution;

namespace CodeInTasks.Application.Abstractions.Interfaces.Services
{
    public interface ISolutionService
    {
        Task<Guid> AddAsync(SolutionCreateDto solutionCreateDto);
        Task UpdateStatusAsync(SolutionStatusUpdateDto statusUpdateDto);

        Task<SolutionViewDto> GetAsync(Guid solutionId);
        Task<IEnumerable<SolutionViewDto>> GetFilteredAsync(SolutionFilterDto filterDto);
    }
}
