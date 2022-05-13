using CodeInTasks.Application.Dtos.Solution;

namespace CodeInTasks.Application.Interfaces.Services
{
    public interface ISolutionService
    {
        Task<Guid> AddAsync(SolutionCreateDto solutionCreateDto);
        Task UpdateStatusAsync(SolutionStatusUpdateDto statusUpdateDto);

        Task<SolutionViewDto> GetAsync(Guid solutionId);
        Task<IEnumerable<SolutionViewDto>> GetAllAsync(SolutionFilterDto filterDto);
    }
}
