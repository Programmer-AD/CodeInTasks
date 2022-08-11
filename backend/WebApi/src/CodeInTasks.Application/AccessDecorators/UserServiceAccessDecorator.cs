using CodeInTasks.Application.Abstractions;
using CodeInTasks.Application.Abstractions.Dtos.User;

namespace CodeInTasks.Application.AccessDecorators
{
    internal class UserServiceAccessDecorator : IUserService
    {
        private readonly IUserService userService;
        private readonly ICurrentUserHolder currentUser;

        public UserServiceAccessDecorator(
            IUserService userService,
            ICurrentUserHolder currentUser)
        {
            this.userService = userService;
            this.currentUser = currentUser;
        }

        public Task CreateAsync(UserCreateDto userCreateDto)
        {
            return userService.CreateAsync(userCreateDto);
        }

        public Task<UserData> GetAsync(Guid userId)
        {
            return userService.GetAsync(userId);
        }

        public Task SetBanAsync(Guid userId, bool isBanned)
        {
            if (currentUser.IsInRole(RoleNames.Manager))
            {
                return userService.SetBanAsync(userId, isBanned);
            }
            else
            {
                throw new AccessDeniedException();
            }
        }

        public Task SetRoleAsync(Guid userId, RoleEnum role, bool isHave)
        {
            if (CanSetRole(role))
            {
                return userService.SetRoleAsync(userId, role, isHave);
            }
            else
            {
                throw new AccessDeniedException();
            }
        }

        public Task<UserSignInResultDto> SignInAsync(string email, string password)
        {
            return userService.SignInAsync(email, password);
        }

        private bool CanSetRole(RoleEnum role)
        {
            return currentUser.IsInRole(RoleNames.Admin)
                || currentUser.IsInRole(RoleNames.Manager)
                && role == RoleEnum.Creator;
        }

    }
}
