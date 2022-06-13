using CodeInTasks.Web.Models.Solution;

namespace CodeInTasks.Builder.Runtime.Abstractions.Interfaces.Services
{
    public interface ISolutionStatusTracer
    {
        /// <summary>
        /// Id of traced solution
        /// </summary>
        Guid SolutionId { get; }

        /// <summary>
        /// Updates solution status at remote
        /// <para>
        /// Throws <see cref="InvalidOperationException"/> 
        /// if <paramref name="solutionStatus"/> has status <see cref="TaskSolutionStatus.Finished"/>.
        /// Instead of this method use <see cref="PublishResultAsync(TaskSolutionResult, Action{SolutionStatusUpdateModel})"/> 
        /// </para>
        /// <para>
        /// Throws <see cref="InvalidOperationException"/> 
        /// If result already published
        /// </para>
        /// </summary>
        /// <exception cref="InvalidOperationException" />
        Task ChangeStatusAsync(TaskSolutionStatus solutionStatus);

        /// <summary>
        /// Publishes solution status <see cref="TaskSolutionStatus.Finished"/> and result to remote
        /// <para>
        /// In <paramref name="configureResultModel"/> you can set additional result information.
        /// Other changes will be discarded
        /// </para>
        /// <para>
        /// Throws <see cref="InvalidOperationException"/> 
        /// If result already published
        /// </para>
        /// </summary>
        /// <param name="configureResultModel">Configuration of additional result information</param>
        /// <exception cref="InvalidOperationException" />
        Task PublishResultAsync(TaskSolutionResult solutionResult, Action<SolutionStatusUpdateModel> configureResultModel);
    }
}
