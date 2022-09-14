using AutoMapper;
using CodeInTasks.WebApi.Models.Task;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeInTasks.Web.Controllers
{
    [ApiController, Route("api/task")]
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
        public async Task<ActionResult<IEnumerable<TaskViewModel>>> GetFilteredAsync(TaskFilterModel filterModel)
        {
            var taskModels = await taskService.GetFilteredAsync(filterModel);

            var taskViewModels = mapper.Map<IEnumerable<TaskViewModel>>(taskModels);

            return Ok(taskViewModels);
        }

        [HttpPost]
        public async Task<ActionResult<TaskCreateResultModel>> AddAsync(TaskCreateModel taskCreateModel)
        {
            var result = await taskService.AddAsync(taskCreateModel);

            return Ok(result);
        }

        [HttpPut("{taskId}")]
        public async Task<ActionResult> UpdateAsync(TaskUpdateModel taskUpdateModel)
        {
            await taskService.UpdateAsync(taskUpdateModel);

            return Ok();
        }

        [HttpDelete("{taskId}")]
        public async Task<ActionResult> DeleteAsync(Guid taskId)
        {
            await taskService.DeleteAsync(taskId);

            return Ok();
        }
    }
}
