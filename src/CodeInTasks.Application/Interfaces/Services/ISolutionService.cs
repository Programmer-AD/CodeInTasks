using CodeInTasks.Application.Dtos.Solution;

namespace CodeInTasks.Application.Interfaces.Services
{
    public interface ISolutionService
    {
        Task<Guid> AddAsync(SolutionCreateDto solution);
        Task UpdateStatusAsync(SolutionStatusUpdateDto solution);

        Task<SolutionViewDto> GetAsync(Guid id);
        Task<IEnumerable<SolutionViewDto>> GetAllAsync(SolutionFilterDto filterDto);
    }
}
