using AutoMapper;
using CodeInTasks.Application.Abstractions.Dtos.User;
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
            var email = signInModel.Email;
            var password = signInModel.Password;

            var signInToken = await identityService.GetSignInTokenAsync(email, password);

            if (signInToken != null)
            {
                var result = new UserSignInResultModel()
                {
                    Token = signInToken,
                    Email = email,
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
            if (CanSetRole(roleManageModel.Role))
            {
                var userId = roleManageModel.UserId;
                var role = roleManageModel.Role;
                var isSetted = roleManageModel.IsSetted;

                await identityService.SetRoleAsync(userId, role, isSetted);

                return Ok();
            }
            else
            {
                return Forbid();
            }
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

        private bool CanSetRole(RoleEnum role)
        {
            return User.IsInRole(RoleNames.Admin)
                || User.IsInRole(RoleNames.Manager) && role == RoleEnum.Creator;
        }
    }
}
