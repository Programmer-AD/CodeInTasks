﻿using AutoMapper;
using CodeInTasks.Application.Dtos.User;
using CodeInTasks.Web.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeInTasks.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IJwtIdentityService identityService;
        private readonly IMapper mapper;

        public UserController(IJwtIdentityService identityService, IMapper mapper)
        {
            this.identityService = identityService;
            this.mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("/signIn")]
        public async Task<ActionResult<UserSignInResultModel>> SignInAsync(UserSignInModel signInModel)
        {
            var username = signInModel.Username;
            var password = signInModel.Password;

            var isSignedIn = await identityService.TrySignInAsync(username, password, out var token);

            if (isSignedIn)
            {
                var result = new UserSignInResultModel()
                {
                    Token = token,
                    Username = username,
                };

                return Ok(result);
            }
            else
            {
                return Unauthorized();
            }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> RegisterAsync(UserCreateModel userCreateModel)
        {
            var userCreateDto = mapper.Map<UserCreateDto>(userCreateModel);

            await identityService.CreateUserAsync(userCreateDto);

            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("info/{username}")]
        public async Task<ActionResult<UserViewModel>> GetUserInfoAsync(Guid userId)
        {
            var userViewDto = await identityService.GetUserInfoAsync(userId);
            
            var userViewModel = mapper.Map<UserViewModel>(userViewDto);

            return Ok(userViewModel);
        }

        [Authorize(Roles = $"{RoleNames.Manager},{RoleNames.Admin}")]
        [HttpPut("role")]
        public async Task<ActionResult> SetRoleAsync(RoleManageModel roleManageModel)
        {
            var userId = roleManageModel.UserId;
            var roleName = roleManageModel.RoleName;
            var isSetted = roleManageModel.IsSetted;

            await identityService.SetRoleAsync(userId, roleName, isSetted);

            return Ok();
        }

        [Authorize(Roles = $"{RoleNames.Manager}")]
        [HttpPut("ban")]
        public async Task<ActionResult> SetBanAsync(BanManageModel banManageModel)
        {
            var userId = banManageModel.UserId;
            var isBanned = banManageModel.IsBanned;

            await identityService.SetBanAsync(userId, isBanned);

            return Ok();
        }
    }
}
