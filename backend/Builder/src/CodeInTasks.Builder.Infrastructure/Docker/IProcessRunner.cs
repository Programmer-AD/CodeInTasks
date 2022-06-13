namespace CodeInTasks.Builder.Infrastructure.Docker
{
    internal interface IProcessRunner
    {
        Task<ProcessRunnerResult> RunProcessAsync(ProcessRunnerArguments arguments);
    }
}