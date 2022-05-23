using CodeInTasks.Application.Abstractions.Dtos.Task;
using CodeInTasks.Application.Filtration;
using CodeInTasks.Application.Filtration.Actions;

namespace CodeInTasks.Application.Tests.Filtration.Actions
{
    [TestFixture]
    public class TaskFiltrationActionsTests
    {
        private TaskFilterDto filterDto;
        private FiltrationPipelineResult<TaskModel> pipelineResult;

        [SetUp]
        public void SetUp()
        {
            filterDto = new();
            pipelineResult = new();
        }

        [Test]
        public void CreatorFilter_WhenCreatorIdIsNull_DontSetFilterExpression()
        {
            filterDto.CreatorId = null;


            TaskFiltrationActions.CreatorFilter(filterDto, pipelineResult);


            pipelineResult.FilterExpression.Should().BeNull();
        }

        [Test]
        public void CreatorFilter_WhenCreatorIdNotNull_SetFilterExpression()
        {
            filterDto.CreatorId = Guid.NewGuid();


            TaskFiltrationActions.CreatorFilter(filterDto, pipelineResult);


            pipelineResult.FilterExpression.Should().NotBeNull();
        }

        [Test]
        public void CategoryFilter_WhenCategoriesIsEmpty_DontSetFilterExpression()
        {
            filterDto.Categories = Array.Empty<TaskCategory>();


            TaskFiltrationActions.CategoryFilter(filterDto, pipelineResult);


            pipelineResult.FilterExpression.Should().BeNull();
        }

        [Test]
        public void CategoryFilter_WhenCategoriesIsNotEmpty_SetFilterExpression()
        {
            filterDto.Categories = new[] { TaskCategory.Implement };


            TaskFiltrationActions.CategoryFilter(filterDto, pipelineResult);


            pipelineResult.FilterExpression.Should().NotBeNull();
        }

        [Test]
        public void RunnerFilter_WhenRunnersIsEmpty_DontSetFilterExpression()
        {
            filterDto.Runners = Array.Empty<RunnerType>();


            TaskFiltrationActions.RunnerFilter(filterDto, pipelineResult);


            pipelineResult.FilterExpression.Should().BeNull();
        }

        [Test]
        public void RunnerFilter_WhenCategoriesIsNotEmpty_SetFilterExpression()
        {
            filterDto.Runners = new[] { RunnerType.Dotnet_6_0 };


            TaskFiltrationActions.RunnerFilter(filterDto, pipelineResult);


            pipelineResult.FilterExpression.Should().NotBeNull();
        }

        [Test]
        public void Ordering_SetsOrderFunction()
        {
            TaskFiltrationActions.Ordering(filterDto, pipelineResult);


            pipelineResult.OrderFunction.Should().NotBeNull();
        }
    }
}
