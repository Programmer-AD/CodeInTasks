using CodeInTasks.Web.Models.Solution;

namespace CodeInTasks.Builder.Runtime.Abstractions.Interfaces.Infrastructure
{
    public interface ISolutionStatusUpdater
    {
        /// <summary>
        /// Makes call of API to update solution with new status
        /// </summary>
        /// <param name="solutionStatus">New status of solution</param>
        Task UpdateStatusAsync(SolutionStatusUpdateModel solutionStatus);
    }
}
