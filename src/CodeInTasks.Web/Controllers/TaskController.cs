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

        public TaskController(ITaskService taskService)
        {
            this.taskService = taskService;
        }

        [AllowAnonymous]
        [HttpGet("{taskId}")]
        public async Task<ActionResult<TaskViewModel>> GetAsync(Guid taskId)
        {
            //TODO: TaskController.GetAsync
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskViewModel>>> GetFilteredAsync(
            TaskFilterModel filterModel)
        {
            //TODO: TaskController.GetFilteredAsync
        }

        [Authorize(Roles = $"{RoleNames.Creator}")]
        [HttpPost]
        public async Task<ActionResult<TaskCreateResultModel>> AddAsync(TaskCreateModel createModel)
        {
            //TODO: TaskController.AddAsync
        }

        [Authorize(Roles = $"{RoleNames.Creator},{RoleNames.Manager}")]
        [HttpPut("{taskId}")]
        public async Task<ActionResult> UpdateAsync(Guid taskId, TaskUpdateModel updateModel)
        {
            //TODO: TaskController.UpdateAsync
        }

        [Authorize(Roles = $"{RoleNames.Creator},{RoleNames.Manager}")]
        [HttpDelete("{taskId}")]
        public async Task<ActionResult> DeleteAsync(Guid taskId)
        {
            //TODO: TaskController.DeleteAsync
        }
    }
}
