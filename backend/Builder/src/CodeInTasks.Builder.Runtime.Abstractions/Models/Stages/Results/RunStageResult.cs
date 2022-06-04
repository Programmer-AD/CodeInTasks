namespace CodeInTasks.Builder.Runtime.Abstractions.Models.Stages.Results
{
    public class RunStageResult : StageResultBase
    {
        public bool IsTaskCompleted { get; set; }
        public int RunTimeMs { get; set; }
    }
}
