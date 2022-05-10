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

        //TODO: UserController
    }
}
