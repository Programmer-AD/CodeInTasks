using System.Security.Claims;

namespace CodeInTasks.Application.Abstractions.Interfaces.Infrastructure
{
    public interface ICurrentUserHolder
    {
        Guid? UserId { get; }

        void Init(ClaimsPrincipal claimsPrincipal);

        bool IsInRole(string roleName);
    }
}
