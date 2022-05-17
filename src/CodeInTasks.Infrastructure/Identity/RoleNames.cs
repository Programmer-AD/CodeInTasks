using CodeInTasks.Application.Dtos.User;

namespace CodeInTasks.Infrastructure.Identity
{
    public class RoleNames
    {
        public const string Creator = "Creator";
        public const string Manager = "Manager";
        public const string Admin = "Admin";
        public const string Builder = "Builder";

        public static string FromEnum(RoleEnum roleEnum)
        {
            var result = roleEnum switch
            {
                RoleEnum.Creator => Creator,
                RoleEnum.Manager => Manager,
                RoleEnum.Admin => Admin,
                RoleEnum.Builder => Builder,
                _ => throw new NotImplementedException(),
            };

            return result;
        }
    }
}
