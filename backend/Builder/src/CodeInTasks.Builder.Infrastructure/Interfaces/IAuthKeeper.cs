using System.Net.Http.Headers;

namespace CodeInTasks.Builder.Infrastructure.Interfaces
{
    public interface IAuthKeeper
    {
        ValueTask<AuthenticationHeaderValue> GetAuthenticationHeaderAsync();
    }
}