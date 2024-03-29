﻿using CodeInTasks.WebApi.Models.User;

namespace CodeInTasks.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IIdentityService identityService;

        public UserService(IIdentityService identityService)
        {
            this.identityService = identityService;
        }

        public Task CreateAsync(UserCreateModel userCreateModel)
        {
            return identityService.CreateUserAsync(userCreateModel);
        }

        public Task<UserData> GetAsync(Guid userId)
        {
            return identityService.GetUserInfoAsync(userId);
        }

        public Task SetBanAsync(Guid userId, bool isBanned)
        {
            return identityService.SetBanAsync(userId, isBanned);
        }

        public Task SetRoleAsync(Guid userId, RoleEnum role, bool isHave)
        {
            return identityService.SetRoleAsync(userId, role, isHave);
        }

        public Task<UserSignInResultModel> SignInAsync(string email, string password)
        {
            return identityService.SignInAsync(email, password);
        }
    }
}
