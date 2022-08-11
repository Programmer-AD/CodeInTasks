using CodeInTasks.Application.Filtration;
using CodeInTasks.Application.Filtration.Actions;
using CodeInTasks.WebApi.Models.Task;

namespace CodeInTasks.Application.Tests.Filtration.Actions
{
    [TestFixture]
    public class TaskFiltrationActionsTests
    {
        private TaskFilterModel filterModel;
        private FiltrationPipelineResult<TaskModel> pipelineResult;

        [SetUp]
        public void SetUp()
        {
            filterModel = new();
            pipelineResult = new();
        }

        [Test]
        public void CreatorFilter_WhenCreatorIdIsNull_DontSetFilterExpression()
        {
            filterModel.CreatorId = null;


            TaskFiltrationActions.CreatorFilter(filterModel, pipelineResult);


            pipelineResult.FilterExpression.Should().BeNull();
        }

        [Test]
        public void CreatorFilter_WhenCreatorIdNotNull_SetFilterExpression()
        {
            filterModel.CreatorId = Guid.NewGuid();


            TaskFiltrationActions.CreatorFilter(filterModel, pipelineResult);


            pipelineResult.FilterExpression.Should().NotBeNull();
        }

        [Test]
        public void CategoryFilter_WhenCategoriesIsEmpty_DontSetFilterExpression()
        {
            filterModel.Categories = Array.Empty<TaskCategory>();


            TaskFiltrationActions.CategoryFilter(filterModel, pipelineResult);


            pipelineResult.FilterExpression.Should().BeNull();
        }

        [Test]
        public void CategoryFilter_WhenCategoriesIsNotEmpty_SetFilterExpression()
        {
            filterModel.Categories = new[] { TaskCategory.Implement };


            TaskFiltrationActions.CategoryFilter(filterModel, pipelineResult);


            pipelineResult.FilterExpression.Should().NotBeNull();
        }

        [Test]
        public void RunnerFilter_WhenRunnersIsEmpty_DontSetFilterExpression()
        {
            filterModel.Runners = Array.Empty<RunnerType>();


            TaskFiltrationActions.RunnerFilter(filterModel, pipelineResult);


            pipelineResult.FilterExpression.Should().BeNull();
        }

        [Test]
        public void RunnerFilter_WhenCategoriesIsNotEmpty_SetFilterExpression()
        {
            filterModel.Runners = new[] { RunnerType.Dotnet_6 };


            TaskFiltrationActions.RunnerFilter(filterModel, pipelineResult);


            pipelineResult.FilterExpression.Should().NotBeNull();
        }

        [Test]
        public void Ordering_SetsOrderFunction()
        {
            TaskFiltrationActions.Ordering(filterModel, pipelineResult);


            pipelineResult.OrderFunction.Should().NotBeNull();
        }
    }
}
