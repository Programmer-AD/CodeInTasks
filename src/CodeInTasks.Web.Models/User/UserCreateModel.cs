namespace CodeInTasks.Web.Models.User
{
    public class UserCreateModel
    {
        [Required]
        [MinLength(DomainConstants.User_Name_MinLength)]
        [MaxLength(DomainConstants.User_Name_MaxLength)]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(DomainConstants.User_Password_MinLength)]
        [MaxLength(DomainConstants.User_Password_MaxLength)]
        public string Password { get; set; }
    }
}
