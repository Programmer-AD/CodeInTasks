using CodeInTasks.Application.Dtos.User;

namespace CodeInTasks.Application.Interfaces.Infrastructure
{
    public interface IJwtIdentityService
    {
        Task CreateUserAsync(UserCreateDto userCreateDto);
        Task SetRoleAsync(Guid userId, RoleEnum role, bool isHave);
        Task SetBanAsync(Guid userId, bool isBanned);

        Task<UserViewDto> GetUserInfoAsync(Guid userId);
        Task<string> GetSignInTokenAsync(string email, string password);
    }
}
