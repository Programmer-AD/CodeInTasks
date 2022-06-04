namespace CodeInTasks.Builder.Runtime.Services
{
    internal class SolutionStatusTracerFactory : ISolutionStatusTracerFactory
    {
        private readonly ISolutionStatusUpdater statusUpdater;

        public SolutionStatusTracerFactory(ISolutionStatusUpdater statusUpdater)
        {
            this.statusUpdater = statusUpdater;
        }

        public ISolutionStatusTracer CreateTracer(Guid solutionId)
        {
            return new SolutionStatusTracer(solutionId, statusUpdater);
        }
    }
}
