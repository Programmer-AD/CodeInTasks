using CodeInTasks.Application.Abstractions.Dtos.User;

namespace CodeInTasks.Application.Abstractions.Interfaces.Services
{
    public interface IUserService
    {
        Task CreateAsync(UserCreateDto userCreateDto);
        Task SetRoleAsync(Guid userId, RoleEnum role, bool isHave);
        Task SetBanAsync(Guid userId, bool isBanned);

        Task<UserData> GetAsync(Guid userId);
        Task<UserSignInResultDto> SignInAsync(string email, string password);
    }
}
