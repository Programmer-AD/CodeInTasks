using AutoMapper;
using CodeInTasks.Application.Dtos.Task;
using CodeInTasks.Web.Models.Task;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeInTasks.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService taskService;
        private readonly IMapper mapper;

        public TaskController(ITaskService taskService, IMapper mapper)
        {
            this.taskService = taskService;
            this.mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("{taskId}")]
        public async Task<ActionResult<TaskViewModel>> GetAsync(Guid taskId)
        {
            var taskViewDto = await taskService.GetAsync(taskId);

            var taskViewModel = mapper.Map<TaskViewModel>(taskViewDto);
            return Ok(taskViewModel);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskViewModel>>> GetFilteredAsync(
            TaskFilterModel filterModel)
        {
            var filterDto = mapper.Map<TaskFilterDto>(filterModel);
            var taskViewDtos = await taskService.GetAllAsync(filterDto);

            var taskViewModels = mapper.Map<IEnumerable<TaskViewModel>>(taskViewDtos);
            return Ok(taskViewModels);
        }

        [Authorize(Roles = $"{RoleNames.Creator}")]
        [HttpPost]
        public async Task<ActionResult<TaskCreateResultModel>> AddAsync(TaskCreateModel taskCreateModel)
        {
            var taskCreateDto = mapper.Map<TaskCreateDto>(taskCreateModel);
            var taskId = await taskService.AddAsync(taskCreateDto);

            var result = new TaskCreateResultModel { TaskId = taskId };
            return Ok(result);
        }

        [Authorize(Roles = $"{RoleNames.Creator},{RoleNames.Manager}")]
        [HttpPut("{taskId}")]
        public async Task<ActionResult> UpdateAsync(Guid taskId, TaskUpdateModel taskUpdateModel)
        {
            var taskUpdateDto = mapper.Map<TaskUpdateDto>(taskUpdateModel);
            await taskService.UpdateAsync(taskUpdateDto);

            return Ok();
        }

        [Authorize(Roles = $"{RoleNames.Creator},{RoleNames.Manager}")]
        [HttpDelete("{taskId}")]
        public async Task<ActionResult> DeleteAsync(Guid taskId)
        {
            await taskService.DeleteAsync(taskId);

            return Ok();
        }
    }
}
