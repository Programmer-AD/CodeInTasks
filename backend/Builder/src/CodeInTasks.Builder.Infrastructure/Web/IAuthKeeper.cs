using System.Net.Http.Headers;

namespace CodeInTasks.Builder.Infrastructure.Web
{
    internal interface IAuthKeeper
    {
        ValueTask<AuthenticationHeaderValue> GetAuthenticationHeaderAsync();
    }
}