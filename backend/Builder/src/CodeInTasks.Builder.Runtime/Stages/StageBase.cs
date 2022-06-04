namespace CodeInTasks.Builder.Runtime.Stages
{
    internal abstract class StageBase<TArguments, TResult> : IStage<TArguments, TResult>
        where TResult : StageResultBase
    {
        public async Task InvokeAsync(TArguments arguments, Func<TResult, Task> onSuccess, Func<TResult, Task> onFail)
        {
            var result = await GetResultAsync(arguments);

            var resultHandler = result.IsSucceded ? onSuccess : onFail;
            if (resultHandler != null)
            {
                await resultHandler(result);
            }

            await CleanAsync(arguments);
        }

        protected abstract Task<TResult> GetResultAsync(TArguments arguments);
        protected abstract Task CleanAsync(TArguments arguments);
    }
}
