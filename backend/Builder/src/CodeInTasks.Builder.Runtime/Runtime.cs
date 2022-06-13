using CodeInTasks.Domain.Enums;
using CodeInTasks.Shared.Queues.Messages;
using CodeInTasks.Web.Models.Solution;

namespace CodeInTasks.Builder.Runtime
{
    public class Runtime : IRuntime
    {
        private readonly ISolutionStatusTracerFactory solutionStatusTracerFactory;
        private readonly IDownloadStage downloadStage;
        private readonly IBuildStage buildStage;
        private readonly IRunStage runStage;

        public Runtime(
            ISolutionStatusTracerFactory solutionStatusTracerFactory,
            IDownloadStage downloadStage,
            IBuildStage buildStage,
            IRunStage runStage)
        {
            this.solutionStatusTracerFactory = solutionStatusTracerFactory;
            this.downloadStage = downloadStage;
            this.buildStage = buildStage;
            this.runStage = runStage;
        }

        public async Task HandleAsync(SolutionCheckQueueMessage checkQueueMessage)
        {
            var solutionId = checkQueueMessage.SolutionId;
            var solutionStatusTracer = solutionStatusTracerFactory.CreateTracer(solutionId);

            await InvokeDownloadStageAsync(solutionStatusTracer, checkQueueMessage);
        }

        private async Task InvokeDownloadStageAsync(
            ISolutionStatusTracer solutionStatusTracer,
            SolutionCheckQueueMessage checkQueueMessage)
        {
            var solutionId = solutionStatusTracer.SolutionId;

            var destinationFolder = GetGitRepositoryFolder(solutionId);

            var downloadArguments = new DownloadStageArguments(
                destinationFolder,
                checkQueueMessage.SolutionRepositoryInfo,
                checkQueueMessage.TestRepositoryInfo);

            await solutionStatusTracer.ChangeStatusAsync(TaskSolutionStatus.Downloading);

            await downloadStage.InvokeAsync(downloadArguments,
                onSuccess: _ => InvokeBuildStageAsync(solutionStatusTracer, downloadArguments.DestinationFolder, checkQueueMessage.Runner),
                onFail: stageResult => PublishStageFailAsync(solutionStatusTracer, TaskSolutionResult.DownloadError, stageResult));
        }

        private async Task InvokeBuildStageAsync(
            ISolutionStatusTracer solutionStatusTracer,
            string repositoryFolder,
            RunnerType runner)
        {
            var solutionId = solutionStatusTracer.SolutionId;
            var instanceName = GetSolutionInstanceName(solutionId);

            var buildArguments = new BuildStageArguments(repositoryFolder, runner, instanceName);

            await solutionStatusTracer.ChangeStatusAsync(TaskSolutionStatus.Building);

            await buildStage.InvokeAsync(buildArguments,
                onSuccess: _ => InvokeRunStageAsync(solutionStatusTracer, instanceName),
                onFail: stageResult => PublishStageFailAsync(solutionStatusTracer, TaskSolutionResult.BuildError, stageResult));
        }

        private async Task InvokeRunStageAsync(
            ISolutionStatusTracer solutionStatusTracer,
            string instanceName)
        {
            var runArguments = new RunStageArguments(instanceName);

            await solutionStatusTracer.ChangeStatusAsync(TaskSolutionStatus.Running);

            await runStage.InvokeAsync(runArguments,
                onSuccess: stageResult => PublishCheckResultAsync(stageResult, solutionStatusTracer),
                onFail: stageResult => PublishStageFailAsync(solutionStatusTracer, TaskSolutionResult.RunError, stageResult));
        }

        private static Task PublishStageFailAsync(ISolutionStatusTracer solutionStatusTracer, TaskSolutionResult solutionResult, StageResultBase stageResult)
        {
            return solutionStatusTracer.PublishResultAsync(
                solutionResult,
                statusModel => ConfigureSolutionResult(statusModel, stageResult));
        }

        private static Task PublishCheckResultAsync(RunStageResult runStageResult, ISolutionStatusTracer solutionStatusTracer)
        {
            var checkResult = runStageResult.IsTaskCompleted ? TaskSolutionResult.Completed : TaskSolutionResult.Failed;

            return solutionStatusTracer.PublishResultAsync(checkResult, statusModel =>
            {
                ConfigureSolutionResult(statusModel, runStageResult);

                statusModel.RunTimeMs = runStageResult.RunTimeMs;
            });
        }

        private static string GetGitRepositoryFolder(Guid solutionId)
        {
            var idString = solutionId.ToString("N");
            var result = Path.Combine(RuntimeConstants.DownloadFolder, idString);

            return result;
        }

        private static string GetSolutionInstanceName(Guid solutionId)
        {
            var idString = solutionId.ToString("N");

            return idString;
        }

        private static void ConfigureSolutionResult(SolutionStatusUpdateModel statusModel, StageResultBase stageResult)
        {
            statusModel.ErrorCode = stageResult.ErrorCode;
            statusModel.ResultAdditionalInfo = stageResult.AdditionalInfo;
        }
    }
}
