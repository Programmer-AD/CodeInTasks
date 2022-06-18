namespace CodeInTasks.Builder.Runtime.Abstractions
{
    public static class ErrorCodes
    {
        public const string Download_MemoryLimitExceed = "DOWNLOAD_MEMORY_LIMIT_EXCEED";
        public const string Download_Error = "DOWNLOAD_ERROR";
        public const string Download_ErrorAdditionalInfo_TestRepository = "TEST_REPOSITORY";
        public const string Download_ErrorAdditionalInfo_SolutionRepository = "SOLUTION_REPOSITORY";

        public const string Build_Configuration_NoConfig = "BUILD_NO_CONFIG";
        public const string Build_Configuration_TooLargeConfig = "BUILD_TOO_LARGE_CONFIG";
        public const string Build_Configuration_WrongConfig = "BUILD_WRONG_CONFIG";

        public const string Build_Timeout = "BUILD_TIMEOUT";
        public const string Build_Error = "BUILD_ERROR";

        public const string Run_Timeout = "RUN_TIMEOUT";
        public const string Run_Error = "RUN_ERROR";
    }
}
