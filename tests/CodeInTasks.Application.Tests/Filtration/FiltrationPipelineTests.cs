using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CodeInTasks.Application.Filtration;

namespace CodeInTasks.Application.Tests.Filtration
{
    [TestFixture]
    public class FiltrationPipelineTests
    {
        private static readonly object filterDto = new();

        private List<FiltrationAction<object, TestModel>> filtrationActions;

        private FiltrationPipeline<object, TestModel> filtrationPipeline;

        [SetUp]
        public void SetUp()
        {
            filtrationActions = new();

            filtrationPipeline = new(filtrationActions);
        }

        [Test]
        public void GetResult_WhenNoActionsProvidden_ReturnsEmptyResult()
        {
            var result = filtrationPipeline.GetResult(filterDto);


            result.Should().NotBeNull();
            result.FilterExpression.Should().BeNull();
            result.OrderFunction.Should().BeNull();
        }

        [Test]
        public void GetResult_InvokesAllActions()
        {
            const int actionCount = 5;
            int invokedCount = 0;
            for (int i = 0; i < actionCount; i++)
            {
                var currentNumber = i;
                filtrationActions.Add((_, _) => invokedCount++);
            }


            var result = filtrationPipeline.GetResult(filterDto);


            invokedCount.Should().Be(actionCount);
        }

        [Test]
        public void GetResult_KeepsActionOrder()
        {
            const int actionCount = 5;
            bool keptActionOrder = true;
            int actionNumber = 0;
            for (int i = 0; i < actionCount; i++)
            {
                var currentActionNumber = i;
                filtrationActions.Add((_, _) => keptActionOrder &= actionNumber++ == currentActionNumber);
            }


            var result = filtrationPipeline.GetResult(filterDto);


            keptActionOrder.Should().BeTrue();
        }

        [Test]
        public void GetResult_UsesSameResultForAllActions()
        {
            static void FilterAction(object _, FiltrationPipelineResult<TestModel> result)
                => result.AddFilter(x => x.SomeNumber > 0);
            static void OrderAction(object _, FiltrationPipelineResult<TestModel> result)
                => result.OrderFunction = queryable => queryable.OrderBy(x => x.SomeNumber);
            filtrationActions.Add(FilterAction);
            filtrationActions.Add(OrderAction);


            var result = filtrationPipeline.GetResult(filterDto);


            result.FilterExpression.Should().NotBeNull();
            result.OrderFunction.Should().NotBeNull();
        }
    }
}
