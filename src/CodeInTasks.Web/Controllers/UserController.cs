using CodeInTasks.Web.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeInTasks.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IJwtIdentityService identityService;

        public UserController(IJwtIdentityService identityService)
        {
            this.identityService = identityService;
        }

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

                return result;
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterAsync(UserCreateModel userCreateModel)
        {
            //TODO: UserController.Register
        }

        [HttpGet("info/{username}")]
        public async Task<ActionResult<UserViewModel>> GetUserInfoAsync(string username)
        {
            //TODO: UserController.GetUserInfo
        }

        [Authorize(Roles = $"{RoleNames.Manager},{RoleNames.Admin}")]
        [HttpPut("role")]
        public async Task<ActionResult> SetRoleAsync(RoleManageModel roleManageModel)
        {
            //TODO: UserController.SetRoleAsync
        }

        [Authorize(Roles = $"{RoleNames.Manager}")]
        [HttpPut("ban")]
        public async Task<ActionResult> SetBanAsync(RoleManageModel roleManageModel)
        {
            //TODO: UserController.SetBanAsync
        }
    }
}
