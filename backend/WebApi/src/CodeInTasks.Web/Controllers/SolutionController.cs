using AutoMapper;
using CodeInTasks.WebApi.Models.Solution;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeInTasks.Web.Controllers
{
    [ApiController, Route("api/solution")]
    public class SolutionController : ControllerBase
    {
        private readonly ISolutionService solutionService;
        private readonly IMapper mapper;

        public SolutionController(ISolutionService solutionService, IMapper mapper)
        {
            this.solutionService = solutionService;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<SolutionCreateResultModel>> AddAsync(SolutionCreateModel solutionCreateModel)
        {
            var result = await solutionService.AddAsync(solutionCreateModel);

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("{solutionId}")]
        public async Task<ActionResult<SolutionViewModel>> GetAsync(Guid solutionId)
        {
            var solution = await solutionService.GetAsync(solutionId);

            var solutionViewModel = mapper.Map<SolutionViewModel>(solution);

            return Ok(solutionViewModel);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SolutionViewModel>>> GetFilteredAsync(SolutionFilterModel filterModel)
        {
            var solutions = await solutionService.GetFilteredAsync(filterModel);

            var solutionViewModels = mapper.Map<IEnumerable<SolutionViewModel>>(solutions);

            return Ok(solutionViewModels);
        }

        [HttpPatch]
        public async Task<ActionResult> UpdateStatusAsync(SolutionStatusUpdateModel statusUpdateModel)
        {
            await solutionService.UpdateStatusAsync(statusUpdateModel);

            return Ok();
        }
    }
}
