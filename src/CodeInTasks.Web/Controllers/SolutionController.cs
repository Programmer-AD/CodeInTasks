using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeInTasks.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolutionController : ControllerBase
    {
        private readonly ISolutionService solutionService;

        public SolutionController(ISolutionService solutionService)
        {
            this.solutionService = solutionService;
        }

        //TODO: SolutionController
    }
}
