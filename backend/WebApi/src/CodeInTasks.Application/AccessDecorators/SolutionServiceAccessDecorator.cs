namespace CodeInTasks.Application.AccessDecorators
{
    internal class SolutionServiceAccessDecorator : ISolutionService
    {
        private readonly ISolutionService solutionService;

        public SolutionServiceAccessDecorator(ISolutionService solutionService)
        {
            this.solutionService = solutionService;
        }
    }
}
