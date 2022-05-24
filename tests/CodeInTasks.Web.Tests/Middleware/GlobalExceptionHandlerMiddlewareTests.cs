using System.Net.Http;
using CodeInTasks.Web.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CodeInTasks.Web.Tests.Middleware
{
    [TestFixture]
    public class GlobalExceptionHandlerMiddlewareTests
    {
        private Mock<ILogger> loggerMock;
        private TestServer server;
        private HttpClient client;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            loggerMock = new Mock<ILogger>();

            server = WebTestHelpers.BuildTestServer(
                app =>
                {
                    app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
                    //TODO: Add exception thrower middleware here
                },
                services => services.AddScoped(_ => loggerMock.Object));

            client = server.CreateClient();
        }

        [TearDown]
        public void TearDown()
        {
            loggerMock.Reset();
        }

        //TODO: GlobalExceptionHandlerMiddlewareTests
    }
}
