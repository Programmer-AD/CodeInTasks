namespace CodeInTasks.Application.Abstractions.Exceptions
{
    [Serializable]
    public class SolutionAlreadyQueuedException : Exception
    {
        public SolutionAlreadyQueuedException() { }

        public SolutionAlreadyQueuedException(string message)
            : base(message) { }

        public SolutionAlreadyQueuedException(Solution solution)
            : base(GetMessage(solution)) { }

        private static string GetMessage(Solution solution)
        {
            return $"Solution already queued! TaskId = {solution.TaskId}, SenderId = {solution.SenderId}";
        }
    }
}
