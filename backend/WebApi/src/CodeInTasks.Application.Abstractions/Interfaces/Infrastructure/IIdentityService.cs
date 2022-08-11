using CodeInTasks.WebApi.Models.User;

namespace CodeInTasks.Application.Abstractions.Interfaces.Infrastructure
{
    public interface IIdentityService
    {
        Task CreateUserAsync(UserCreateModel userCreateModel);
        Task SetRoleAsync(Guid userId, RoleEnum role, bool isHave);
        Task SetBanAsync(Guid userId, bool isBanned);

        Task<UserData> GetUserInfoAsync(Guid userId);
        Task<UserSignInResultModel> SignInAsync(string email, string password);
    }
}
