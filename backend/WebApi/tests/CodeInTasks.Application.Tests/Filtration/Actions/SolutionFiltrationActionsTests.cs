using CodeInTasks.Application.Abstractions.Dtos.Solution;
using CodeInTasks.Application.Filtration;
using CodeInTasks.Application.Filtration.Actions;

namespace CodeInTasks.Application.Tests.Filtration.Actions
{
    [TestFixture]
    public class SolutionFiltrationActionsTests
    {
        private SolutionFilterDto filterDto;
        private FiltrationPipelineResult<Solution> pipelineResult;

        [SetUp]
        public void SetUp()
        {
            filterDto = new();
            pipelineResult = new();
        }

        [Test]
        public void SenderFilter_WhenSenderIdIsNull_DontSetFilterExpression()
        {
            filterDto.SenderId = null;


            SolutionFiltrationActions.SenderFilter(filterDto, pipelineResult);


            pipelineResult.FilterExpression.Should().BeNull();
        }

        [Test]
        public void SenderFilter_WhenSenderIdNotNull_SetFilterExpression()
        {
            filterDto.SenderId = Guid.NewGuid();


            SolutionFiltrationActions.SenderFilter(filterDto, pipelineResult);


            pipelineResult.FilterExpression.Should().NotBeNull();
        }

        [Test]
        public void TaskFilter_WhenTaskIdIsNull_DontSetFilterExpression()
        {
            filterDto.TaskId = null;


            SolutionFiltrationActions.TaskFilter(filterDto, pipelineResult);


            pipelineResult.FilterExpression.Should().BeNull();
        }

        [Test]
        public void TaskFilter_WhenTaskIdNotNull_SetFilterExpression()
        {
            filterDto.TaskId = Guid.NewGuid();


            SolutionFiltrationActions.TaskFilter(filterDto, pipelineResult);


            pipelineResult.FilterExpression.Should().NotBeNull();
        }

        [Test]
        public void ResultsFilter_WhenResultsIsEmpty_DontSetFilterExpression()
        {
            filterDto.Results = Array.Empty<TaskSolutionResult?>();


            SolutionFiltrationActions.ResultsFilter(filterDto, pipelineResult);


            pipelineResult.FilterExpression.Should().BeNull();
        }

        [Test]
        public void ResultsFilter_WhenResultsIsNotEmpty_SetFilterExpression()
        {
            filterDto.Results = new TaskSolutionResult?[] { TaskSolutionResult.Completed };


            SolutionFiltrationActions.ResultsFilter(filterDto, pipelineResult);


            pipelineResult.FilterExpression.Should().NotBeNull();
        }

        [Test]
        public void Ordering_SetsOrderFunction()
        {
            SolutionFiltrationActions.Ordering(filterDto, pipelineResult);


            pipelineResult.OrderFunction.Should().NotBeNull();
        }
    }
}
