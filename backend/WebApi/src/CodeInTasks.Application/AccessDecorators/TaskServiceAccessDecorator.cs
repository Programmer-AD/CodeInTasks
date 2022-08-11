namespace CodeInTasks.Application.AccessDecorators
{
    internal class TaskServiceAccessDecorator : ITaskService
    {
        private readonly ITaskService taskService;

        public TaskServiceAccessDecorator(ITaskService taskService)
        {
            this.taskService = taskService;
        }
    }
}
