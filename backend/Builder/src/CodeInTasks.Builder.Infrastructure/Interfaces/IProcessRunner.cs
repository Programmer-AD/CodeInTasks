using CodeInTasks.Builder.Infrastructure.Docker;

namespace CodeInTasks.Builder.Infrastructure.Interfaces
{
    internal interface IProcessRunner
    {
        Task<ProcessRunnerResult> RunProcessAsync(ProcessRunnerArguments arguments);
    }
}