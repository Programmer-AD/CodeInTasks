namespace CodeInTasks.Builder.Runtime.Abstractions.Interfaces.Services
{
    public interface ISolutionStatusTracerFactory
    {
        ISolutionStatusTracer CreateTracer(Guid solutionId);
    }
}
