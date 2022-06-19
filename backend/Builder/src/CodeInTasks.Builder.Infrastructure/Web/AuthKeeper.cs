using System.Net.Http.Headers;
using System.Net.Http.Json;
using CodeInTasks.Builder.Infrastructure.Interfaces;
using CodeInTasks.Builder.Runtime.Abstractions;
using CodeInTasks.Web.Models.User;

namespace CodeInTasks.Builder.Infrastructure.Web
{
    internal class AuthKeeper : IAuthKeeper
    {
        private const string AuthScheme = "Bearer";

        private readonly HttpClient httpClient;
        private readonly SolutionStatusUpdaterConfig statusUpdaterConfig;

        private readonly Uri destinationUri;

        private Cache cache;
        private Task<AuthenticationHeaderValue> getResultAndCacheTask;

        public AuthKeeper(
            HttpClient httpClient,
            SolutionStatusUpdaterConfig statusUpdaterConfig)
        {
            this.httpClient = httpClient;
            this.statusUpdaterConfig = statusUpdaterConfig;

            destinationUri = GetDestinationUri(statusUpdaterConfig.ServerUri);
        }

        public ValueTask<AuthenticationHeaderValue> GetAuthenticationHeaderAsync()
        {
            if (IsCacheValid())
            {
                return ValueTask.FromResult(cache.HeaderResult);
            }

            if (getResultAndCacheTask == null)
            {
                getResultAndCacheTask = GetResultAndCacheAsync();
            }

            return new ValueTask<AuthenticationHeaderValue>(getResultAndCacheTask);
        }

        private bool IsCacheValid()
        {
            var result = DateTime.UtcNow < cache.ExpireDate;

            return result;
        }

        private async Task<AuthenticationHeaderValue> GetResultAndCacheAsync()
        {
            var signInResult = await SignInAsync();

            var result = new AuthenticationHeaderValue(AuthScheme, signInResult.Token);

            UpdateCache(signInResult.ExpirationDate, result);

            return result;
        }

        private async Task<UserSignInResultModel> SignInAsync()
        {
            var signInModel = new UserSignInModel()
            {
                Email = statusUpdaterConfig.UserName,
                Password = statusUpdaterConfig.Password
            };
            using var content = JsonContent.Create(signInModel);

            using var sendResult = await httpClient.PostAsync(destinationUri, content);
            sendResult.EnsureSuccessStatusCode();

            var result = await sendResult.Content.ReadFromJsonAsync<UserSignInResultModel>();

            return result;
        }

        private void UpdateCache(DateTime tokenExpire, AuthenticationHeaderValue result)
        {
            var cacheExpire = tokenExpire.AddSeconds(-RuntimeConstants.SolutionStatusUpdater_AuthExpireSecondsReserve);

            cache = new Cache(cacheExpire, result);
        }

        private static Uri GetDestinationUri(string serverBaseUri)
        {
            var baseUri = new Uri(serverBaseUri);
            var destinationUri = new Uri(baseUri, RuntimeConstants.SolutionStatusUpdater_AuthRelativeSendPath);

            return destinationUri;
        }

        private record struct Cache(DateTime ExpireDate, AuthenticationHeaderValue HeaderResult);
    }
}
