using AutoMapper;
using CodeInTasks.Application.Abstractions.Dtos.User;
using CodeInTasks.WebApi.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeInTasks.Web.Controllers
{
    [Route("api/user"), ApiController]
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
        public async Task<ActionResult<UserViewModel>> GetAsync(Guid userId)
        {
            var userViewDto = await userService.GetAsync(userId);

            var userViewModel = mapper.Map<UserViewModel>(userViewDto);

            return Ok(userViewModel);
        }

        [HttpPut("role")]
        public async Task<ActionResult> SetRoleAsync(RoleManageModel roleManageModel)
        {
            var userId = roleManageModel.UserId;
            var role = roleManageModel.Role;
            var isSetted = roleManageModel.IsSetted;

            await userService.SetRoleAsync(userId, role, isSetted);

            return Ok();
        }

        [HttpPut("ban")]
        public async Task<ActionResult> SetBanAsync(BanManageModel banManageModel)
        {
            var userId = banManageModel.UserId;
            var isBanned = banManageModel.IsBanned;

            await userService.SetBanAsync(userId, isBanned);

            return Ok();
        }
    }
}
