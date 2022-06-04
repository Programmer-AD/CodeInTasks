namespace CodeInTasks.Builder.Runtime.Abstractions.Interfaces.Stages
{
    public interface IStage<TArguments, TResult>
        where TResult : StageResultBase
    {
        Task InvokeAsync(TArguments arguments, Func<TResult, Task> onSuccess, Func<TResult, Task> onFail);
    }
}
