using AutoMapper;
using CodeInTasks.Application.Abstractions.Interfaces.Services;
using CodeInTasks.Web.Controllers;

namespace CodeInTasks.Web.Tests.Controllers
{
    [TestFixture]
    public class SolutionControllerTests
    {
        private Mock<ISolutionService> solutionServiceMock;
        private Mock<IMapper> mapperMock;

        private SolutionController solutionController;

        [SetUp]
        public void SetUp()
        {
            solutionServiceMock = new Mock<ISolutionService>();
            mapperMock = new Mock<IMapper>();

            solutionController = new SolutionController(
                solutionServiceMock.Object,
                mapperMock.Object);
        }

        //TODO: SolutionControllerTests
    }
}
