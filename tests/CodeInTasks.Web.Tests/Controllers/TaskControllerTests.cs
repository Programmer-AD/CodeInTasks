using AutoMapper;
using CodeInTasks.Application.Abstractions.Interfaces.Services;
using CodeInTasks.Web.Controllers;

namespace CodeInTasks.Web.Tests.Controllers
{
    [TestFixture]
    public class TaskControllerTests
    {
        private Mock<ITaskService> taskServiceMock;
        private Mock<IMapper> mapperMock;

        private TaskController taskController;

        [SetUp]
        public void SetUp()
        {
            taskServiceMock = new();
            mapperMock = new();

            taskController = new(
                taskServiceMock.Object,
                mapperMock.Object);
        }

        //TODO: TaskControllerTests
    }
}
