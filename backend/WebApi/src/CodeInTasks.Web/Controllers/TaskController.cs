using AutoMapper;
using CodeInTasks.Application.Abstractions.Dtos.Task;
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
            var taskModel = await taskService.GetAsync(taskId);

            var taskViewModel = mapper.Map<TaskViewModel>(taskModel);
            return Ok(taskViewModel);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskViewModel>>> GetFilteredAsync(
            TaskFilterModel filterModel)
        {
            var filterDto = mapper.Map<TaskFilterDto>(filterModel);
            var taskModels = await taskService.GetFilteredAsync(filterDto);

            var taskViewModels = mapper.Map<IEnumerable<TaskViewModel>>(taskModels);
            return Ok(taskViewModels);
        }

        [Authorize(Roles = $"{RoleNames.Creator}")]
        [HttpPost]
        public async Task<ActionResult<TaskCreateResultModel>> AddAsync(TaskCreateModel taskCreateModel)
        {
            var taskCreateDto = mapper.Map<TaskCreateDto>(taskCreateModel);
            taskCreateDto.CreatorId = User.GetUserId();

            var taskId = await taskService.AddAsync(taskCreateDto);

            var result = new TaskCreateResultModel { TaskId = taskId };
            return Ok(result);
        }

        [Authorize(Roles = $"{RoleNames.Creator},{RoleNames.Manager}")]
        [HttpPut("{taskId}")]
        public async Task<ActionResult> UpdateAsync(Guid taskId, TaskUpdateModel taskUpdateModel)
        {
            var canManageTask = await CanManageTask(taskId);

            if (canManageTask)
            {
                var taskUpdateDto = mapper.Map<TaskUpdateDto>(taskUpdateModel);
                taskUpdateDto.Id = taskId;

                await taskService.UpdateAsync(taskUpdateDto);

                return Ok();
            }
            else
            {
                return Forbid();
            }
        }

        [Authorize(Roles = $"{RoleNames.Creator},{RoleNames.Manager}")]
        [HttpDelete("{taskId}")]
        public async Task<ActionResult> DeleteAsync(Guid taskId)
        {
            var canManageTask = await CanManageTask(taskId);

            if (canManageTask)
            {
                await taskService.DeleteAsync(taskId);

                return Ok();
            }
            else
            {
                return Forbid();
            }
        }

        private async Task<bool> CanManageTask(Guid taskId)
        {
            var userId = User.GetUserId();

            var result = User.IsInRole(RoleNames.Manager)
                || User.IsInRole(RoleNames.Creator)
                && await taskService.IsOwnerAsync(taskId, userId);

            return result;
        }
    }
}
