using CodeInTasks.Application.Abstractions.Dtos.User;

namespace CodeInTasks.Application.Abstractions.Interfaces.Infrastructure
{
    public interface IIdentityService
    {
        Task CreateUserAsync(UserCreateDto userCreateDto);
        Task SetRoleAsync(Guid userId, RoleEnum role, bool isHave);
        Task SetBanAsync(Guid userId, bool isBanned);

        Task<UserViewDto> GetUserInfoAsync(Guid userId);
        Task<UserSignInResultDto> SignInAsync(string email, string password);
    }
}
