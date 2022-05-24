using System.Threading;
using System.Threading.Tasks;
using CodeInTasks.Web.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CodeInTasks.Web.Tests.Middleware
{
    [TestFixture]
    public class ChangeSaverMiddlewareTests
    {
        [Test]
        public async Task CallsSaveChangesAsync()
        {
            var dbContextMock = new Mock<DbContext>();

            var server = WebTestHelpers.BuildTestServer(
                app => app.UseMiddleware<ChangeSaverMiddleware>(),
                services => services.AddScoped(_ => dbContextMock.Object));

            var client = server.CreateClient();


            await client.GetAsync(string.Empty);


            dbContextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
