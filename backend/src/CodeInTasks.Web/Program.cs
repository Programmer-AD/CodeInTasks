using CodeInTasks.Application;
using CodeInTasks.Infrastructure;
using CodeInTasks.Web.Filters;

namespace CodeInTasks.Web
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            WebApplication.CreateBuilder(args)
                .SetupServices()
                .Build()
                .Setup()
                .Run();
        }
    }

    internal static class AppExtensions
    {
        public static WebApplicationBuilder SetupServices(this WebApplicationBuilder builder)
        {
            var config = builder.Configuration;
            builder.Services
                .AddApplication(config)
                .AddInfrastructure(config)
                .AddWeb(config);

            return builder;
        }

        public static WebApplication Setup(this WebApplication app)
        {
            app.UseExceptionHandler();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            return app;
        }
    }
}