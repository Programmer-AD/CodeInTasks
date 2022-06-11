using CodeInTasks.Web.Models.Solution;

namespace CodeInTasks.Builder.Infrastructure.Web
{
    internal class SolutionStatusUpdater : ISolutionStatusUpdater
    {
        private readonly HttpClient httpClient;
        private readonly IAuthorizationKeeper authorizationKeeper;

        public SolutionStatusUpdater(HttpClient httpClient, IAuthorizationKeeper authorizationKeeper)
        {
            this.httpClient = httpClient;
            this.authorizationKeeper = authorizationKeeper;
        }

        public Task UpdateStatusAsync(SolutionStatusUpdateModel solutionStatus)
        {
            
        }


    }
}
