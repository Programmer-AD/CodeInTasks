using System.Net.Http.Headers;

namespace CodeInTasks.Builder.Infrastructure.Interfaces
{
    internal interface IAuthKeeper
    {
        ValueTask<AuthenticationHeaderValue> GetAuthenticationHeaderAsync();
    }
}