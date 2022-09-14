using CodeInTasks.Builder.Infrastructure.Docker;

namespace CodeInTasks.Builder.Infrastructure.Interfaces
{
    public interface IProcessRunner
    {
        Task<ProcessRunnerResult> RunProcessAsync(ProcessRunnerArguments arguments);
    }
}