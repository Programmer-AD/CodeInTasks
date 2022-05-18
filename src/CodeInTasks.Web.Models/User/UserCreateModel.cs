namespace CodeInTasks.Web.Models.User
{
    public class UserCreateModel
    {
        [Required]
        [MinLength(DomainConstants.UserData_Name_MinLength)]
        [MaxLength(DomainConstants.UserData_Name_MaxLength)]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(DomainConstants.User_Password_MinLength)]
        [MaxLength(DomainConstants.User_Password_MaxLength)]
        public string Password { get; set; }
    }
}
