using AutoMapper;
using CodeInTasks.Application.Abstractions.Dtos.Task;
using CodeInTasks.Application.Abstractions.Interfaces.Filtration;
using CodeInTasks.Application.Abstractions.Interfaces.Infrastructure.Persistance;
using CodeInTasks.Application.Services;

namespace CodeInTasks.Application.Tests.Services
{
    [TestFixture]
    public class TaskServiceTests
    {
        private Mock<IRepository<TaskModel>> taskRepositoryMock;
        private Mock<IFiltrationPipeline<TaskFilterDto, TaskModel>> filtrationPipelineMock;
        private Mock<IMapper> mapperMock;

        private TaskService taskService;

        [SetUp]
        public void SetUp()
        {
            taskRepositoryMock = new();
            filtrationPipelineMock = new();
            mapperMock = new();

            taskService = new(
               taskRepositoryMock.Object,
               filtrationPipelineMock.Object,
               mapperMock.Object);
        }

        //TODO: TaskServiceTests
    }
}
