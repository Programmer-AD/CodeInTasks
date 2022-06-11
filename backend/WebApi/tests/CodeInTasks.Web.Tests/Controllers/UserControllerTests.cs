using AutoMapper;
using CodeInTasks.Application.Abstractions.Interfaces.Infrastructure;
using CodeInTasks.Web.Controllers;

namespace CodeInTasks.Web.Tests.Controllers
{
    [TestFixture]
    public class UserControllerTests
    {
        private Mock<IIdentityService> identityServiceMock;
        private Mock<IMapper> mapperMock;

        private UserController userController;

        [SetUp]
        public void SetUp()
        {
            identityServiceMock = new();
            mapperMock = new();

            userController = new(
                identityServiceMock.Object,
                mapperMock.Object);
        }

        //TODO: UserControllerTests
    }
}
