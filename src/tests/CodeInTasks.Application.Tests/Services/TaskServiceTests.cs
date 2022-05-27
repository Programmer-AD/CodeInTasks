﻿using System.Linq.Expressions;
using System.Threading.Tasks;
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
        private static readonly Guid taskId = Guid.NewGuid();
        private static readonly Guid userId = Guid.NewGuid();
        private static readonly TaskModel taskModel = new();
        private static readonly TaskCreateDto taskCreateDto = new();
        private static readonly TaskUpdateDto taskUpdateDto = new();
        private static readonly TaskFilterDto filterDto = new();


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

            SetupMappings();
            ServiceTestHelpers.SetupFiltrationPipelineMock(filtrationPipelineMock);
        }

        [Test]
        public async Task AddAsync_AddToRepository()
        {
            await taskService.AddAsync(taskCreateDto);


            taskRepositoryMock.Verify(x => x.AddAsync(It.IsAny<TaskModel>()));
        }

        [Test]
        public async Task DeleteAsync_DeleteFromRepository()
        {
            SetEntityDeletedResult(true);


            await taskService.DeleteAsync(taskId);


            taskRepositoryMock.Verify(x => x.DeleteAsync(It.IsAny<Guid>()));
        }

        [Test]
        public async Task DeleteAsync_WhenNotDeleted_ThrowEntityNotFoundException()
        {
            SetEntityDeletedResult(false);


            await taskService.Invoking(x => x.DeleteAsync(taskId))
                .Should().ThrowAsync<EntityNotFoundException>();
        }

        [Test]
        public async Task DeleteAsync_WhenDeleted_DontThrowException()
        {
            SetEntityDeletedResult(true);


            await taskService.Invoking(x => x.DeleteAsync(taskId))
                .Should().NotThrowAsync();
        }

        [Test]
        public async Task GetFilteredAsync_UseFiltrationPipeline()
        {
            await taskService.GetFilteredAsync(filterDto);


            filtrationPipelineMock.Verify(x => x.GetResult(It.IsAny<TaskFilterDto>()), Times.Once);
        }

        [Test]
        public async Task GetFilteredAsync_GetFilteredFromRepository()
        {
            await taskService.GetFilteredAsync(filterDto);


            taskRepositoryMock.Verify(x => x.GetFilteredAsync(It.IsAny<RepositoryFilter<TaskModel>>()), Times.Once);
        }

        [Test]
        public async Task GetAsync_WhenEntityNotFound_ThrowEntityNotFoundException()
        {
            SetCanFoundEntity(false);


            await taskService.Invoking(x => x.GetAsync(taskId))
                .Should().ThrowAsync<EntityNotFoundException>();
        }

        [Test]
        public async Task GetAsync_WhenEntityFound_DontThrowException()
        {
            SetCanFoundEntity(true);


            await taskService.Invoking(x => x.GetAsync(taskId))
                .Should().NotThrowAsync();
        }

        [Test]
        public async Task GetAsync_GetEntityByIdFromRepository()
        {
            SetCanFoundEntity(true);


            await taskService.GetAsync(taskId);


            taskRepositoryMock.Verify(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<bool>()), Times.Once);
        }

        [Test]
        public async Task UpdateAsync_WhenEntityNotFound_ThrowEntityNotFoundException()
        {
            SetCanFoundEntity(false);


            await taskService.Invoking(x => x.UpdateAsync(taskUpdateDto))
                .Should().ThrowAsync<EntityNotFoundException>();
        }

        [Test]
        public async Task UpdateAsync_WhenEntityNotFound_DontUpdateAtRepository()
        {
            SetCanFoundEntity(false);


            await CallHelpers.ForceCallAsync<EntityNotFoundException>(() => taskService.UpdateAsync(taskUpdateDto));


            taskRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<TaskModel>()), Times.Never);
        }

        [Test]
        public async Task UpdateAsync_WhenEntityFound_DontThrowException()
        {
            SetCanFoundEntity(true);


            await taskService.Invoking(x => x.UpdateAsync(taskUpdateDto))
                .Should().NotThrowAsync();
        }

        [Test]
        public async Task UpdateAsync_UpdateAtRepository()
        {
            SetCanFoundEntity(true);


            await taskService.UpdateAsync(taskUpdateDto);


            taskRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<TaskModel>()), Times.Once);
        }

        [Test]
        public async Task IsOwnerAsync_SearchesUsingRepositoryAny()
        {
            await taskService.IsOwnerAsync(taskId, userId);


            taskRepositoryMock.Verify(x => x.AnyAsync(It.IsAny<Expression<Predicate<TaskModel>>>(), It.IsAny<bool>()), Times.Once);
        }


        private void SetupMappings()
        {
            mapperMock
                .Setup(x => x.Map<TaskModel>(It.IsAny<TaskCreateDto>()))
                .Returns(taskModel);
        }

        private void SetEntityDeletedResult(bool isDeleted)
        {
            taskRepositoryMock
                .Setup(x => x.DeleteAsync(It.IsAny<Guid>()))
                .ReturnsAsync(isDeleted);
        }

        private void SetCanFoundEntity(bool canFound)
        {
            var result = canFound ? taskModel : null;

            ServiceTestHelpers.SetRepositoryGetResult(taskRepositoryMock, result);
        }
    }
}