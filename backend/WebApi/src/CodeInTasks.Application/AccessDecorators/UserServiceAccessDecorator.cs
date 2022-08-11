namespace CodeInTasks.Application.AccessDecorators
{
    internal class UserServiceAccessDecorator : IUserService
    {
        private readonly IUserService userService;

        public UserServiceAccessDecorator(IUserService userService)
        {
            this.userService = userService;
        }
    }
}
