using AutoMapper;
using CodeInTasks.Application.Abstractions.Dtos.User;
using CodeInTasks.Domain.Enums;
using CodeInTasks.Web.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeInTasks.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("signIn")]
        public async Task<ActionResult<UserSignInResultModel>> SignInAsync(UserSignInModel signInModel)
        {
            var email = signInModel.Email;
            var password = signInModel.Password;

            var signInResult = await userService.SignInAsync(email, password);

            if (signInResult != null)
            {
                var result = mapper.Map<UserSignInResultModel>(signInResult);

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

            await userService.CreateAsync(userCreateDto);

            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("{userId}")]
        public async Task<ActionResult<UserViewModel>> GetUserInfoAsync(Guid userId)
        {
            var userViewDto = await userService.GetAsync(userId);

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

                await userService.SetRoleAsync(userId, role, isSetted);

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

            await userService.SetBanAsync(userId, isBanned);

            return Ok();
        }

        private bool CanSetRole(RoleEnum role)
        {
            return User.IsInRole(RoleNames.Admin)
                || User.IsInRole(RoleNames.Manager)
                && role == RoleEnum.Creator;
        }
    }
}
