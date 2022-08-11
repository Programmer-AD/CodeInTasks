using CodeInTasks.Application.Abstractions;
using CodeInTasks.WebApi.Models.Solution;

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

        public Task<Guid> AddAsync(SolutionCreateModel solutionCreateModel)
        {
            return solutionService.AddAsync(solutionCreateModel);
        }

        public Task<Solution> GetAsync(Guid solutionId)
        {
            return solutionService.GetAsync(solutionId);
        }

        public Task<IEnumerable<Solution>> GetFilteredAsync(SolutionFilterModel filterModel)
        {
            return solutionService.GetFilteredAsync(filterModel);
        }

        public Task UpdateStatusAsync(SolutionStatusUpdateModel statusUpdateModel)
        {
            if (currentUser.IsInRole(RoleNames.Builder))
            {
                return solutionService.UpdateStatusAsync(statusUpdateModel);
            }
            else
            {
                throw new AccessDeniedException();
            }
        }
    }
}
