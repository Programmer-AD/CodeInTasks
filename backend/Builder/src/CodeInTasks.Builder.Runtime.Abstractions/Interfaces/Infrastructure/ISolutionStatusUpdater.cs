using CodeInTasks.Web.Models.Solution;

namespace CodeInTasks.Builder.Runtime.Abstractions.Interfaces.Infrastructure
{
    public interface ISolutionStatusUpdater
    {
        Task UpdateStatusAsync(SolutionStatusUpdateModel solutionStatus);
    }
}
