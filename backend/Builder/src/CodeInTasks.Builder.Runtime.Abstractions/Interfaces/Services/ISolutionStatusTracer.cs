using CodeInTasks.Web.Models.Solution;

namespace CodeInTasks.Builder.Runtime.Abstractions.Interfaces.Services
{
    public interface ISolutionStatusTracer
    {
        Task ChangeStatusAsync(TaskSolutionStatus solutionStatus);

        Task PublishResultAsync(Action<SolutionStatusUpdateModel> configureResultModel);
    }
}
