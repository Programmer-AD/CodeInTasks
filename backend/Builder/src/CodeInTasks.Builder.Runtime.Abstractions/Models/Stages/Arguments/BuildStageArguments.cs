namespace CodeInTasks.Builder.Runtime.Abstractions.Models.Stages.Arguments
{
    public class BuildStageArguments
    {
        public string FolderPath { get; set; }
        public RunnerType Runner { get; set; }
        public string InstanceName { get; set; }
    }
}
