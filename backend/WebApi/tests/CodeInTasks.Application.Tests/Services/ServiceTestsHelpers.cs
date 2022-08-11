using System.Linq.Expressions;
using CodeInTasks.Application.Abstractions.Interfaces.Filtration;
using CodeInTasks.Application.Abstractions.Interfaces.Infrastructure.Persistance;

namespace CodeInTasks.Application.Tests.Services
{
    internal static class ServiceTestHelpers
    {
        public static void SetRepositoryGetResult<T>(Mock<IRepository<T>> repositoryMock, T result)
            where T : ModelBase
        {
            repositoryMock
                .Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<bool>()))
                .ReturnsAsync(result);

            repositoryMock
                .Setup(x => x.GetAsync(It.IsAny<Expression<Predicate<T>>>(), It.IsAny<bool>()))
                .ReturnsAsync(result);
        }

        public static void SetupFiltrationPipelineMock<TFilterModel, TEntity>(Mock<IFiltrationPipeline<TFilterModel, TEntity>> filtrationPipelineMock)
        {
            var pipelineResultMock = new Mock<IFiltrationPipelineResult<TEntity>>();

            filtrationPipelineMock
                .Setup(x => x.GetResult(It.IsAny<TFilterModel>()))
                .Returns(pipelineResultMock.Object);
        }
    }
}