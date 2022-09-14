using Microsoft.AspNetCore.Mvc.Filters;

namespace CodeInTasks.Web.Filters
{
    public class CurrentUserHolderInitFilter : IAsyncResourceFilter
    {
        private readonly ICurrentUserHolder currentUserHolder;

        public CurrentUserHolderInitFilter(ICurrentUserHolder currentUserHolder)
        {
            this.currentUserHolder = currentUserHolder;
        }

        public Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            var principal = context.HttpContext.User;

            currentUserHolder.Init(principal);

            return next();
        }
    }
}
