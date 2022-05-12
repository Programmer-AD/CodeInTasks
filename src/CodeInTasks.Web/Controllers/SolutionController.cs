using CodeInTasks.Web.Models.Solution;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddAsync(SolutionCreateModel createModel)
        {
            //TODO: SolutionController.AddAsync
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SolutionViewModel>>> GetFilteredAsync(
            SolutionFilterModel filterModel)
        {
            //TODO: SolutionController.GetFilteredAsync
        }

        [Authorize(Roles = RoleNames.Builder)]
        [HttpPut]
        public async Task<ActionResult> UpdateStatusAsync(SolutionStatusUpdateModel statusUpdateModel)
        {
            //TODO: SolutionController.UpdateStatusAsync
        }

    }
}
