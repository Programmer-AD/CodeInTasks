using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using CodeInTasks.Application.Abstractions.Dtos.Solution;
using CodeInTasks.Application.Abstractions.Interfaces.Filtration;
using CodeInTasks.Application.Abstractions.Interfaces.Infrastructure;
using CodeInTasks.Application.Abstractions.Interfaces.Infrastructure.Persistance;
using CodeInTasks.Application.Services;
using CodeInTasks.Shared.TestHelpers;

namespace CodeInTasks.Application.Tests.Services
{
    [TestFixture]
    public class SolutionServiceTests
    {
        private static readonly Guid solutionId = Guid.NewGuid();
        private static readonly SolutionCreateDto solutionCreateDto = new();
        private static readonly Solution solution = new();
        private static readonly SolutionQueueDto solutionQueueDto = new();
        private static readonly SolutionStatusUpdateDto statusUpdateDto = new();
        private static readonly SolutionFilterDto filterDto = new();


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

            SetupMappings();
            ServiceTestHelpers.SetupFiltrationPipelineMock(filtrationPipelineMock);
        }

        [Test]
        public async Task AddAsync_WhenSolutionAlreadyQueued_ThrowSolutionAlreadyQueuedException()
        {
            SetSolutionAlreadyQueued(true);


            await solutionService.Invoking(x => x.AddAsync(solutionCreateDto))
                .Should().ThrowAsync<SolutionAlreadyQueuedException>();
        }

        [Test]
        public async Task AddAsync_WhenSolutionAlreadyQueued_DontAddToRepository()
        {
            SetSolutionAlreadyQueued(true);


            await CallHelpers.ForceCallAsync<SolutionAlreadyQueuedException>(() => solutionService.AddAsync(solutionCreateDto));


            solutionRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Solution>()), Times.Never);
        }

        [Test]
        public async Task AddAsync_WhenSolutionAlreadyQueued_DontEnqueueSolutionCheck()
        {
            SetSolutionAlreadyQueued(true);


            await CallHelpers.ForceCallAsync<SolutionAlreadyQueuedException>(() => solutionService.AddAsync(solutionCreateDto));


            checkQueueMock.Verify(x => x.EnqueueSolutionCheck(It.IsAny<SolutionQueueDto>()), Times.Never);
        }

        [Test]
        public async Task AddAsync_WhenSolutionNotQueued_DontThrowException()
        {
            SetSolutionAlreadyQueued(false);


            await solutionService.Invoking(x => x.AddAsync(solutionCreateDto))
                .Should().NotThrowAsync();
        }

        [Test]
        public async Task AddAsync_AddToRepository()
        {
            SetSolutionAlreadyQueued(false);


            await solutionService.AddAsync(solutionCreateDto);


            solutionRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Solution>()), Times.Once);
        }

        [Test]
        public async Task AddAsync_EnqueueSolutionCheck()
        {
            SetSolutionAlreadyQueued(false);


            await solutionService.AddAsync(solutionCreateDto);


            checkQueueMock.Verify(x => x.EnqueueSolutionCheck(It.IsAny<SolutionQueueDto>()), Times.Once);
        }

        [Test]
        public async Task GetFilteredAsync_UseFiltrationPipeline()
        {
            await solutionService.GetFilteredAsync(filterDto);


            filtrationPipelineMock.Verify(x => x.GetResult(It.IsAny<SolutionFilterDto>()), Times.Once);
        }

        [Test]
        public async Task GetFilteredAsync_GetFilteredFromRepository()
        {
            await solutionService.GetFilteredAsync(filterDto);


            solutionRepositoryMock.Verify(x => x.GetFilteredAsync(It.IsAny<RepositoryFilter<Solution>>()), Times.Once);
        }

        [Test]
        public async Task GetAsync_WhenEntityNotFound_ThrowEntityNotFoundException()
        {
            SetCanFoundEntityById(false);


            await solutionService.Invoking(x => x.GetAsync(solutionId))
                .Should().ThrowAsync<EntityNotFoundException>();
        }

        [Test]
        public async Task GetAsync_WhenEntityFound_DontThrowException()
        {
            SetCanFoundEntityById(true);


            await solutionService.Invoking(x => x.GetAsync(solutionId))
                .Should().NotThrowAsync();
        }

        [Test]
        public async Task GetAsync_GetByIdFromRepository()
        {
            SetCanFoundEntityById(true);


            await solutionService.GetAsync(solutionId);


            solutionRepositoryMock.Verify(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<bool>()));
        }

        [Test]
        public async Task UpdateStatusAsync_WhenEntityNotFound_ThrowEntityNotFoundException()
        {
            SetCanFoundEntityById(false);


            await solutionService.Invoking(x => x.UpdateStatusAsync(statusUpdateDto))
                .Should().ThrowAsync<EntityNotFoundException>();
        }

        [Test]
        public async Task UpdateStatusAsync_WhenEntityNotFound_DontUpdateAtRepository()
        {
            SetCanFoundEntityById(false);


            await CallHelpers.ForceCallAsync<EntityNotFoundException>(() => solutionService.UpdateStatusAsync(statusUpdateDto));


            solutionRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Solution>()), Times.Never);
        }

        [Test]
        public async Task UpdateStatusAsync_WhenEntityFound_DontThrowException()
        {
            SetCanFoundEntityById(true);


            await solutionService.Invoking(x => x.UpdateStatusAsync(statusUpdateDto))
                .Should().NotThrowAsync();
        }

        [Test]
        public async Task UpdateStatusAsync_UpdateAtRepository()
        {
            SetCanFoundEntityById(true);


            await solutionService.UpdateStatusAsync(statusUpdateDto);


            solutionRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Solution>()), Times.Once);
        }


        private void SetupMappings()
        {
            mapperMock
                .Setup(x => x.Map<Solution>(It.IsAny<SolutionCreateDto>()))
                .Returns(solution);

            mapperMock
                .Setup(x => x.Map<SolutionQueueDto>(It.IsAny<Solution>()))
                .Returns(solutionQueueDto);
        }

        private void SetSolutionAlreadyQueued(bool isQueued)
        {
            solutionRepositoryMock
                .Setup(x => x.AnyAsync(It.IsAny<Expression<Predicate<Solution>>>(), It.IsAny<bool>()))
                .ReturnsAsync(isQueued);
        }

        private void SetCanFoundEntityById(bool canFound)
        {
            var result = canFound ? solution : null;

            ServiceTestHelpers.SetRepositoryGetResult(solutionRepositoryMock, result);
        }
    }
}
