using CodeInTasks.Domain.Enums;
using CodeInTasks.Web.Models.Solution;

namespace CodeInTasks.Builder.Runtime.Services
{
    internal class SolutionStatusTracer : ISolutionStatusTracer
    {
        private readonly SolutionStatusUpdateModel solutionStatusModel;
        private readonly ISolutionStatusUpdater statusUpdater;

        public Guid SolutionId { get; }

        public SolutionStatusTracer(Guid solutionId, ISolutionStatusUpdater statusUpdater)
        {
            SolutionId = solutionId;
            this.statusUpdater = statusUpdater;

            solutionStatusModel = new() { Id = solutionId, };
        }

        public Task ChangeStatusAsync(TaskSolutionStatus solutionStatus)
        {
            AssertStatusIsSettable(solutionStatus);
            AssertCanUpdateResult();

            solutionStatusModel.Status = solutionStatus;

            return statusUpdater.UpdateStatusAsync(solutionStatusModel);
        }

        public Task PublishResultAsync(TaskSolutionResult solutionResult, Action<SolutionStatusUpdateModel> configureResultModel)
        {
            AssertCanUpdateResult();

            configureResultModel(solutionStatusModel);

            solutionStatusModel.Id = SolutionId;
            solutionStatusModel.Result = solutionResult;
            solutionStatusModel.Status = TaskSolutionStatus.Finished;
            solutionStatusModel.FinishTime = DateTime.UtcNow;

            return statusUpdater.UpdateStatusAsync(solutionStatusModel);
        }

        private static void AssertStatusIsSettable(TaskSolutionStatus solutionStatus)
        {
            if (solutionStatus == TaskSolutionStatus.Finished)
            {
                throw new InvalidOperationException("Cant set status as \"Finished\"! Use method PublishResult instead");
            }
        }

        private void AssertCanUpdateResult()
        {
            if (solutionStatusModel.Status == TaskSolutionStatus.Finished)
            {
                throw new InvalidOperationException("Cant update status model: result already published!");
            }
        }
    }
}
