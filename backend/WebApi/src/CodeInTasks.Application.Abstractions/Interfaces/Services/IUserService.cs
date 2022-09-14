using CodeInTasks.WebApi.Models.User;

namespace CodeInTasks.Application.Abstractions.Interfaces.Services
{
    public interface IUserService
    {
        Task CreateAsync(UserCreateModel userCreateModel);
        Task SetRoleAsync(Guid userId, RoleEnum role, bool isHave);
        Task SetBanAsync(Guid userId, bool isBanned);

        Task<UserData> GetAsync(Guid userId);
        Task<UserSignInResultModel> SignInAsync(string email, string password);
    }
}
