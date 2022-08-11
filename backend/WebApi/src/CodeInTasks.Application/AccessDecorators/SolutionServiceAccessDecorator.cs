using CodeInTasks.Application.Abstractions;
using CodeInTasks.Application.Abstractions.Dtos.Solution;

namespace CodeInTasks.Application.AccessDecorators
{
    internal class SolutionServiceAccessDecorator : ISolutionService
    {
        private readonly ISolutionService solutionService;
        private readonly ICurrentUserHolder currentUser;

        public SolutionServiceAccessDecorator(
            ISolutionService solutionService,
            ICurrentUserHolder currentUser)
        {
            this.solutionService = solutionService;
            this.currentUser = currentUser;
        }

        public Task<Guid> AddAsync(SolutionCreateDto solutionCreateDto)
        {
            return solutionService.AddAsync(solutionCreateDto);
        }

        public Task<Solution> GetAsync(Guid solutionId)
        {
            return solutionService.GetAsync(solutionId);
        }

        public Task<IEnumerable<Solution>> GetFilteredAsync(SolutionFilterDto filterDto)
        {
            return solutionService.GetFilteredAsync(filterDto);
        }

        public Task UpdateStatusAsync(SolutionStatusUpdateDto statusUpdateDto)
        {
            if (currentUser.IsInRole(RoleNames.Builder))
            {
                return solutionService.UpdateStatusAsync(statusUpdateDto);
            }
            else
            {
                throw new AccessDeniedException();
            }
        }
    }
}
