using System.Net.Http.Json;
using CodeInTasks.Builder.Runtime.Abstractions;
using CodeInTasks.Web.Models.Solution;

namespace CodeInTasks.Builder.Infrastructure.Web
{
    internal class SolutionStatusUpdater : ISolutionStatusUpdater
    {
        private readonly HttpClient httpClient;
        private readonly AuthKeeper authKeeper;
        private readonly SolutionStatusUpdaterConfig config;

        private readonly Uri destinationUri;

        public SolutionStatusUpdater(
            HttpClient httpClient,
            AuthKeeper authKeeper,
            SolutionStatusUpdaterConfig config)
        {
            this.httpClient = httpClient;
            this.authKeeper = authKeeper;
            this.config = config;

            destinationUri = GetDestinationUri(config.ServerUri);
        }

        public async Task UpdateStatusAsync(SolutionStatusUpdateModel solutionStatus)
        {
            using var content = JsonContent.Create(solutionStatus);

            using var requestMessage = new HttpRequestMessage(HttpMethod.Patch, destinationUri)
            {
                Content = content
            };

            var authHeader = await authKeeper.GetAuthenticationHeaderAsync();
            requestMessage.Headers.Authorization = authHeader;

            using var result = await httpClient.SendAsync(requestMessage);

            result.EnsureSuccessStatusCode();
        }

        private static Uri GetDestinationUri(string serverBaseUri)
        {
            var baseUri = new Uri(serverBaseUri);
            var destinationUri = new Uri(baseUri, RuntimeConstants.SolutionStatusUpdater_RelativeSendPath);

            return destinationUri;
        }
    }
}
