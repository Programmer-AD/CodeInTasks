using AutoMapper;
using CodeInTasks.Application.Dtos.Solution;
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
        public async Task<ActionResult> AddAsync(SolutionCreateModel solutionCreateModel)
        {
            var solutionCreateDto = mapper.Map<SolutionCreateDto>(solutionCreateModel);

            await solutionService.AddAsync(solutionCreateDto);

            return Ok();
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SolutionViewModel>>> GetFilteredAsync(
            SolutionFilterModel filterModel)
        {
            var solutionFilterDto = mapper.Map<SolutionFilterDto>(filterModel);

            var solutionViewDtos = await solutionService.GetAllAsync(solutionFilterDto);

            var solutionViewModels = mapper.Map<IEnumerable<SolutionViewModel>>(solutionViewDtos);

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
