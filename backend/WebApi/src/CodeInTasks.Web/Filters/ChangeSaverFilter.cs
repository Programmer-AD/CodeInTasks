using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace CodeInTasks.Web.Filters
{
    public class ChangeSaverFilter : IAsyncActionFilter
    {
        private readonly DbContext dbContext;

        public ChangeSaverFilter(DbContext dbContext)
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
