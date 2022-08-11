using System.Security.Claims;

namespace CodeInTasks.Application.Abstractions.Interfaces.Infrastructure
{
    public interface ICurrentUserHolder
    {
        ClaimsPrincipal Principal { get; }

        Guid? UserId { get; }

        void Init(ClaimsPrincipal claimsPrincipal);

        bool IsInRole(string roleName);
    }
}
