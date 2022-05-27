namespace CodeInTasks.Domain
{
    public static class DomainConstants
    {
        public const int TaskModel_Title_MinLength = 4;
        public const int TaskModel_Title_MaxLength = 32;

        public const int TaskModel_Description_MinLength = 0;
        public const int TaskModel_Description_MaxLength = 1024;


        public const int Solution_ResultAdditionalInfo_MinLength = 0;
        public const int Solution_ResultAdditionalInfo_MaxLength = 1024;


        public const int User_Password_MinLength = 8;
        public const int User_Password_MaxLength = 40;

        public const int UserData_Name_MinLength = 3;
        public const int UserData_Name_MaxLength = 60;



        public const int RepositoryUrl_MinLength = 4;
        public const int RepositoryUrl_MaxLength = 1024;

        public const int RepositoryAccessToken_MinLength = 4;
        public const int RepositoryAccessToken_MaxLength = 256;
    }
}
