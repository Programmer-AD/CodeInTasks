using AutoMapper;
using CodeInTasks.Application.Abstractions.Dtos.Solution;
using CodeInTasks.Application.Abstractions.Interfaces.Filtration;
using CodeInTasks.Application.Abstractions.Interfaces.Infrastructure;
using CodeInTasks.Application.Abstractions.Interfaces.Infrastructure.Persistance;
using CodeInTasks.Application.Services;

namespace CodeInTasks.Application.Tests.Services
{
    [TestFixture]
    public class SolutionServiceTests
    {
        private Mock<IRepository<Solution>> solutionRepositoryMock;
        private Mock<ISolutionCheckQueue> checkQueueMock;
        private Mock<IFiltrationPipeline<SolutionFilterDto, Solution>> filtrationPipelineMock;
        private Mock<IMapper> mapperMock;

        private SolutionService solutionService;

        [SetUp]
        public void SetUp()
        {
            solutionRepositoryMock = new();
            checkQueueMock = new();
            filtrationPipelineMock = new();
            mapperMock = new();

            solutionService = new(
               solutionRepositoryMock.Object,
               checkQueueMock.Object,
               filtrationPipelineMock.Object,
               mapperMock.Object);
        }

        //TODO: SolutionServiceTests
    }
}
