using AutoMapper;
using CodeInTasks.Application.Abstractions.Dtos.Solution;
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
        private readonly IMapper mapper;

        public SolutionController(ISolutionService solutionService, IMapper mapper)
        {
            this.solutionService = solutionService;
            this.mapper = mapper;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<SolutionCreateResultModel>> AddAsync(
            SolutionCreateModel solutionCreateModel)
        {
            var solutionCreateDto = mapper.Map<SolutionCreateDto>(solutionCreateModel);
            solutionCreateDto.SenderId = User.GetUserId();

            var solutionId = await solutionService.AddAsync(solutionCreateDto);

            var result = new SolutionCreateResultModel { SolutionId = solutionId };
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<SolutionViewModel>> GetAsync(Guid solutionId)
        {
            var solution = await solutionService.GetAsync(solutionId);

            var solutionViewModel = mapper.Map<SolutionViewModel>(solution);
            return Ok(solutionViewModel);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SolutionViewModel>>> GetFilteredAsync(
            SolutionFilterModel filterModel)
        {
            var solutionFilterDto = mapper.Map<SolutionFilterDto>(filterModel);

            var solutions = await solutionService.GetFilteredAsync(solutionFilterDto);

            var solutionViewModels = mapper.Map<IEnumerable<SolutionViewModel>>(solutions);
            return Ok(solutionViewModels);
        }

        [Authorize(Roles = RoleNames.Builder)]
        [HttpPut]
        public async Task<ActionResult> UpdateStatusAsync(SolutionStatusUpdateModel statusUpdateModel)
        {
            var statusUpdateDto = mapper.Map<SolutionStatusUpdateDto>(statusUpdateModel);

            await solutionService.UpdateStatusAsync(statusUpdateDto);

            return Ok();
        }
    }
}
