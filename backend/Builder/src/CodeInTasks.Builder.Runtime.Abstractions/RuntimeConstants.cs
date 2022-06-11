namespace CodeInTasks.Builder.Runtime.Abstractions
{
    public static class RuntimeConstants
    {
        public const string TaskConfigFileName = "taskConfig.json";

        public const string DownloadFolder = "./data/tmp";

        public const long Git_MaxDownloadSizeBytes = 4 * 1024 * 1024; //4 MB

        public const string SolutionStatusUpdater_RelativeSendPath = "/api/solution";
        public const int SolutionStatusUpdater_AuthExpireSecondsReserve = 5;
        public const string SolutionStatusUpdater_AuthRelativeSendPath = "/api/user/signIn";
    }
}
