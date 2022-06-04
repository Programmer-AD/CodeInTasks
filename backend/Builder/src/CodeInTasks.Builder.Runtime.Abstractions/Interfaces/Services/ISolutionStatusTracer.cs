using CodeInTasks.Web.Models.Solution;

namespace CodeInTasks.Builder.Runtime.Abstractions.Interfaces.Services
{
    public interface ISolutionStatusTracer
    {
        Guid SolutionId { get;  }
        Task ChangeStatusAsync(TaskSolutionStatus solutionStatus);
        Task PublishResultAsync(TaskSolutionResult solutionResult, Action<SolutionStatusUpdateModel> configureResultModel);
    }
}
