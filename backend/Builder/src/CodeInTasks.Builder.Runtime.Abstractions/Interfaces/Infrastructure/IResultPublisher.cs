using CodeInTasks.Web.Models.Solution;

namespace CodeInTasks.Builder.Runtime.Abstractions.Interfaces.Infrastructure
{
    public interface IResultPublisher
    {
        Task PublishAsync(SolutionStatusUpdateModel solutionStatus);
    }
}
