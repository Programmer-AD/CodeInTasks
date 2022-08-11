using CodeInTasks.Application.Filtration;
using CodeInTasks.Application.Filtration.Actions;
using CodeInTasks.WebApi.Models.Solution;

namespace CodeInTasks.Application.Tests.Filtration.Actions
{
    [TestFixture]
    public class SolutionFiltrationActionsTests
    {
        private SolutionFilterModel filterModel;
        private FiltrationPipelineResult<Solution> pipelineResult;

        [SetUp]
        public void SetUp()
        {
            filterModel = new();
            pipelineResult = new();
        }

        [Test]
        public void SenderFilter_WhenSenderIdIsNull_DontSetFilterExpression()
        {
            filterModel.SenderId = null;


            SolutionFiltrationActions.SenderFilter(filterModel, pipelineResult);


            pipelineResult.FilterExpression.Should().BeNull();
        }

        [Test]
        public void SenderFilter_WhenSenderIdNotNull_SetFilterExpression()
        {
            filterModel.SenderId = Guid.NewGuid();


            SolutionFiltrationActions.SenderFilter(filterModel, pipelineResult);


            pipelineResult.FilterExpression.Should().NotBeNull();
        }

        [Test]
        public void TaskFilter_WhenTaskIdIsNull_DontSetFilterExpression()
        {
            filterModel.TaskId = null;


            SolutionFiltrationActions.TaskFilter(filterModel, pipelineResult);


            pipelineResult.FilterExpression.Should().BeNull();
        }

        [Test]
        public void TaskFilter_WhenTaskIdNotNull_SetFilterExpression()
        {
            filterModel.TaskId = Guid.NewGuid();


            SolutionFiltrationActions.TaskFilter(filterModel, pipelineResult);


            pipelineResult.FilterExpression.Should().NotBeNull();
        }

        [Test]
        public void ResultsFilter_WhenResultsIsEmpty_DontSetFilterExpression()
        {
            filterModel.Results = Array.Empty<TaskSolutionResult?>();


            SolutionFiltrationActions.ResultsFilter(filterModel, pipelineResult);


            pipelineResult.FilterExpression.Should().BeNull();
        }

        [Test]
        public void ResultsFilter_WhenResultsIsNotEmpty_SetFilterExpression()
        {
            filterModel.Results = new TaskSolutionResult?[] { TaskSolutionResult.Completed };


            SolutionFiltrationActions.ResultsFilter(filterModel, pipelineResult);


            pipelineResult.FilterExpression.Should().NotBeNull();
        }

        [Test]
        public void Ordering_SetsOrderFunction()
        {
            SolutionFiltrationActions.Ordering(filterModel, pipelineResult);


            pipelineResult.OrderFunction.Should().NotBeNull();
        }
    }
}
