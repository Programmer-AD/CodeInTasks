namespace CodeInTasks.SharedTestHelpers
{
    public static class CallHelpers
    {
        public static void ForceCall<TPossibleException>(Action action)
            where TPossibleException : Exception
        {
            try
            {
                action();
            }
            catch (TPossibleException) { }
        }

        public static async Task ForceCallAsync<TPossibleException>(Func<Task> asyncAction)
            where TPossibleException : Exception
        {
            try
            {
                await asyncAction();
            }
            catch (TPossibleException) { }
        }
    }
}