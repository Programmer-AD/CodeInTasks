using CodeInTasks.Application.Abstractions;
using CodeInTasks.WebApi.Models.Task;

namespace CodeInTasks.Application.AccessDecorators
{
    internal class TaskServiceAccessDecorator : ITaskService
    {
        private readonly ITaskService taskService;
        private readonly ICurrentUserHolder currentUser;

        public TaskServiceAccessDecorator(
            ITaskService taskService,
            ICurrentUserHolder currentUser)
        {
            this.taskService = taskService;
            this.currentUser = currentUser;
        }

        public Task<Guid> AddAsync(TaskCreateModel taskCreateModel)
        {
            if (currentUser.IsInRole(RoleNames.Creator))
            {
                return taskService.AddAsync(taskCreateModel);
            }
            else
            {
                throw new AccessDeniedException();
            }
        }

        public async Task DeleteAsync(Guid taskId)
        {
            var canManageTask = await CanManageTaskAsync(taskId);

            if (canManageTask)
            {
                await taskService.DeleteAsync(taskId);
            }
            else
            {
                throw new AccessDeniedException();
            }
        }

        public Task<TaskModel> GetAsync(Guid taskId)
        {
            return taskService.GetAsync(taskId);
        }

        public Task<IEnumerable<TaskModel>> GetFilteredAsync(TaskFilterModel filterModel)
        {
            return taskService.GetFilteredAsync(filterModel);
        }

        public Task<bool> IsOwnerAsync(Guid taskId, Guid userId)
        {
            return taskService.IsOwnerAsync(taskId, userId);
        }

        public async Task UpdateAsync(TaskUpdateModel taskUpdateModel)
        {
            var canManageTask = await CanManageTaskAsync(taskUpdateModel.Id);

            if (canManageTask)
            {
                await taskService.UpdateAsync(taskUpdateModel);
            }
            else
            {
                throw new AccessDeniedException();
            }
        }

        private async Task<bool> CanManageTaskAsync(Guid taskId)
        {
            var userId = currentUser.UserId.Value;

            var result = currentUser.IsInRole(RoleNames.Manager)
                || currentUser.IsInRole(RoleNames.Creator)
                && await taskService.IsOwnerAsync(taskId, userId);

            return result;
        }
    }
}
