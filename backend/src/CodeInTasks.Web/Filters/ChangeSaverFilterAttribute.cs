using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace CodeInTasks.Web.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ChangeSaverFilterAttribute : Attribute, IAsyncActionFilter
    {
        private readonly DbContext dbContext;

        public ChangeSaverFilterAttribute(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await next();
            await dbContext.SaveChangesAsync();
        }
    }
}
