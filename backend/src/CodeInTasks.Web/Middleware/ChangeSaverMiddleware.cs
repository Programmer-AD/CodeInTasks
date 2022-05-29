using Microsoft.EntityFrameworkCore;

namespace CodeInTasks.Web.Middleware
{
    public class ChangeSaverMiddleware
    {
        private readonly RequestDelegate next;

        public ChangeSaverMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, DbContext dbContext)
        {
            await next(context);

            await dbContext.SaveChangesAsync();
        }
    }
}
