using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace CodeInTasks.Web.Tests
{
    internal static class WebTestHelpers
    {

        public static TestServer BuildTestServer(Action<IApplicationBuilder> configure, Action<IServiceCollection> configureServices = null)
        {
            var hostBuilder = new WebHostBuilder();

            if (configureServices != null)
            {
                hostBuilder.ConfigureServices(configureServices);
            }

            hostBuilder.Configure(appBuilder => configure(appBuilder));

            var testServer = new TestServer(hostBuilder);
            return testServer;
        }
    }
}