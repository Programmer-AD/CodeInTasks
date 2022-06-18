namespace CodeInTasks.Builder.Runtime.Abstractions
{
    public static class RuntimeConstants
    {
        public const string TaskConfig_FileName = "taskConfig.json";
        public const long TaskConfig_MaxSizeBytes = 512 * 1024; //512 KB

        public const string Download_Folder = "./data/tmp";
        public const long Download_MaxSizeBytes = 4 * 1024 * 1024; //4 MB

        public static readonly TimeSpan BuildTimeout = TimeSpan.FromSeconds(20);
        public static readonly TimeSpan RunTimeout = TimeSpan.FromSeconds(10);

        public const string SolutionStatusUpdater_RelativeSendPath = "/api/solution";
        public const int SolutionStatusUpdater_AuthExpireSecondsReserve = 5;
        public const string SolutionStatusUpdater_AuthRelativeSendPath = "/api/user/signIn";

        public const int DockerProvider_ContainerMemoryLimitMB = 256;

        public const int Docker_ApplicationErrorExitCode = 1;
        public const string Git_FolderName = ".git";
    }
}
