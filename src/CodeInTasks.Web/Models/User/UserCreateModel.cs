namespace CodeInTasks.Web.Models.User
{
    public class UserCreateModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(IdentityConstants.Password_RequiredLength)]
        public string Password { get; set; }
    }
}
