namespace CodeInTasks.Builder.Runtime.Abstractions.Interfaces.Services
{
    public interface ISolutionStatusTracerFactory
    {
        /// <summary>
        /// Creates <see cref="ISolutionStatusTracer"/> for specified <paramref name="solutionId"/>
        /// </summary>
        /// <param name="solutionId">Id of existing solution</param>
        ISolutionStatusTracer CreateTracer(Guid solutionId);
    }
}
