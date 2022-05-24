using System.Linq.Expressions;
using CodeInTasks.Application.Filtration;

namespace CodeInTasks.Application.Tests.Filtration
{
    [TestFixture]
    public class FiltrationPipelineResultTests
    {
        //Different parameter names to be sure that expressions merged correctly
        private static readonly Expression<Predicate<TestModel>> SomeNumberBiggerThan5 = model => model.SomeNumber > 5;
        private static readonly Expression<Predicate<TestModel>> SomeNumberLessThan10 = x => x.SomeNumber < 10;

        private FiltrationPipelineResult<TestModel> filtrationPipelineResult;

        [SetUp]
        public void SetUp()
        {
            filtrationPipelineResult = new();
        }

        [Test]
        public void AddFilter_WhenAddingFirstFilter_SetItAsFilterExpression()
        {
            var filterExpression = SomeNumberBiggerThan5;


            filtrationPipelineResult.AddFilter(filterExpression);


            filtrationPipelineResult.FilterExpression.Should().Be(filterExpression);
        }

        [TestCase(2, false)]
        [TestCase(7, true)]
        [TestCase(12, false)]
        public void AddFilter_WhenAddingSecondFilter_MergeFiltersCorrectly(int someNumberValue, bool expectedPredicateResult)
        {
            var testModel = new TestModel() { SomeNumber = someNumberValue };
            var firstExpression = SomeNumberBiggerThan5;
            var secondExpression = SomeNumberLessThan10;
            filtrationPipelineResult.AddFilter(firstExpression);


            filtrationPipelineResult.AddFilter(secondExpression);


            var resultingFilterPredicate = filtrationPipelineResult.FilterExpression.Compile();
            var predicateResult = resultingFilterPredicate(testModel);
            predicateResult.Should().Be(expectedPredicateResult);
        }
    }
}
