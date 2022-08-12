using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using CodeInTasks.Application.Abstractions.Interfaces.Enqueuers;
using CodeInTasks.Application.Abstractions.Interfaces.Filtration;
using CodeInTasks.Application.Abstractions.Interfaces.Infrastructure;
using CodeInTasks.Application.Abstractions.Interfaces.Infrastructure.Persistance;
using CodeInTasks.Application.Services;
using CodeInTasks.WebApi.Models.Solution;

namespace CodeInTasks.Application.Tests.Services
{
    [TestFixture]
    public class SolutionServiceTests
    {
        private static readonly Guid solutionId = Guid.NewGuid();
        private static readonly SolutionCreateModel solutionCreateModel = new();
        private static readonly Solution solution = new();
        private static readonly SolutionStatusUpdateModel statusUpdateModel = new();
        private static readonly SolutionFilterModel filterModel = new();


        private Mock<IRepository<Solution>> solutionRepositoryMock;
        private Mock<ISolutionCheckEnqueuer> checkQueueMock;
        private Mock<IFiltrationPipeline<SolutionFilterModel, Solution>> filtrationPipelineMock;
        private Mock<ICurrentUserHolder> currentUserHolderMock;
        private Mock<IMapper> mapperMock;

        private SolutionService solutionService;

        [SetUp]
        public void SetUp()
        {
            solutionRepositoryMock = new();
            checkQueueMock = new();
            filtrationPipelineMock = new();
            currentUserHolderMock = new();
            mapperMock = new();

            solutionService = new(
               solutionRepositoryMock.Object,
               checkQueueMock.Object,
               filtrationPipelineMock.Object,
               currentUserHolderMock.Object,
               mapperMock.Object);

            SetupMappings();
            ServiceTestHelpers.SetupFiltrationPipelineMock(filtrationPipelineMock);
        }

        [Test]
        public async Task AddAsync_WhenSolutionAlreadyQueued_ThrowSolutionAlreadyQueuedException()
        {
            ServiceTestHelpers.InitCurrentUserId(currentUserHolderMock);
            SetSolutionAlreadyQueued(true);


            await solutionService.Invoking(x => x.AddAsync(solutionCreateModel))
                .Should().ThrowAsync<SolutionAlreadyQueuedException>();
        }

        [Test]
        public async Task AddAsync_WhenSolutionAlreadyQueued_DontAddToRepository()
        {
            ServiceTestHelpers.InitCurrentUserId(currentUserHolderMock);
            SetSolutionAlreadyQueued(true);


            await CallHelpers.ForceCallAsync<SolutionAlreadyQueuedException>(() => solutionService.AddAsync(solutionCreateModel));


            solutionRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Solution>()), Times.Never);
        }

        [Test]
        public async Task AddAsync_WhenSolutionAlreadyQueued_DontEnqueueSolutionCheck()
        {
            ServiceTestHelpers.InitCurrentUserId(currentUserHolderMock);
            SetSolutionAlreadyQueued(true);


            await CallHelpers.ForceCallAsync<SolutionAlreadyQueuedException>(() => solutionService.AddAsync(solutionCreateModel));


            checkQueueMock.Verify(x => x.EnqueueSolutionCheck(It.IsAny<Solution>()), Times.Never);
        }

        [Test]
        public async Task AddAsync_WhenSolutionNotQueued_DontThrowException()
        {
            ServiceTestHelpers.InitCurrentUserId(currentUserHolderMock);
            SetSolutionAlreadyQueued(false);


            await solutionService.Invoking(x => x.AddAsync(solutionCreateModel))
                .Should().NotThrowAsync();
        }

        [Test]
        public async Task AddAsync_AddToRepository()
        {
            ServiceTestHelpers.InitCurrentUserId(currentUserHolderMock);
            SetSolutionAlreadyQueued(false);


            await solutionService.AddAsync(solutionCreateModel);


            solutionRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Solution>()), Times.Once);
        }

        [Test]
        public async Task AddAsync_EnqueueSolutionCheck()
        {
            ServiceTestHelpers.InitCurrentUserId(currentUserHolderMock);
            SetSolutionAlreadyQueued(false);


            await solutionService.AddAsync(solutionCreateModel);


            checkQueueMock.Verify(x => x.EnqueueSolutionCheck(It.IsAny<Solution>()), Times.Once);
        }

        [Test]
        public async Task GetFilteredAsync_UseFiltrationPipeline()
        {
            await solutionService.GetFilteredAsync(filterModel);


            filtrationPipelineMock.Verify(x => x.GetResult(It.IsAny<SolutionFilterModel>()), Times.Once);
        }

        [Test]
        public async Task GetFilteredAsync_GetFilteredFromRepository()
        {
            await solutionService.GetFilteredAsync(filterModel);


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


            await solutionService.Invoking(x => x.UpdateStatusAsync(statusUpdateModel))
                .Should().ThrowAsync<EntityNotFoundException>();
        }

        [Test]
        public async Task UpdateStatusAsync_WhenEntityNotFound_DontUpdateAtRepository()
        {
            SetCanFoundEntityById(false);


            await CallHelpers.ForceCallAsync<EntityNotFoundException>(() => solutionService.UpdateStatusAsync(statusUpdateModel));


            solutionRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Solution>()), Times.Never);
        }

        [Test]
        public async Task UpdateStatusAsync_WhenEntityFound_DontThrowException()
        {
            SetCanFoundEntityById(true);


            await solutionService.Invoking(x => x.UpdateStatusAsync(statusUpdateModel))
                .Should().NotThrowAsync();
        }

        [Test]
        public async Task UpdateStatusAsync_UpdateAtRepository()
        {
            SetCanFoundEntityById(true);


            await solutionService.UpdateStatusAsync(statusUpdateModel);


            solutionRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Solution>()), Times.Once);
        }


        private void SetupMappings()
        {
            mapperMock
                .Setup(x => x.Map<Solution>(It.IsAny<SolutionCreateModel>()))
                .Returns(solution);
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
