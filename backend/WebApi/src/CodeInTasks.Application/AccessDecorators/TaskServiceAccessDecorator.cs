using CodeInTasks.Application.Abstractions;
using CodeInTasks.Application.Abstractions.Dtos.Task;

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

        public Task<Guid> AddAsync(TaskCreateDto taskCreateDto)
        {
            if (currentUser.IsInRole(RoleNames.Creator))
            {
                return taskService.AddAsync(taskCreateDto);
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

        public Task<IEnumerable<TaskModel>> GetFilteredAsync(TaskFilterDto filterDto)
        {
            return taskService.GetFilteredAsync(filterDto);
        }

        public Task<bool> IsOwnerAsync(Guid taskId, Guid userId)
        {
            return taskService.IsOwnerAsync(taskId, userId);
        }

        public async Task UpdateAsync(TaskUpdateDto taskUpdateDto)
        {
            var canManageTask = await CanManageTaskAsync(taskUpdateDto.Id);

            if (canManageTask)
            {
                await taskService.UpdateAsync(taskUpdateDto);
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
