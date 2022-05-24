using System.Linq.Expressions;

namespace CodeInTasks.Infrastructure.Persistance.Tests
{
    [TestFixture]
    public class ExpressionConvertExtensionsTests
    {
        [Test]
        public void PredicateToFunc_MakesWorkingExpression()
        {
            var checkNumber = 2;
            Expression<Predicate<int>> predicateExpression = x => x > 0;


            var funcExpression = predicateExpression.ConvertToFunc();


            var func = funcExpression.Compile();
            var funcResult = func(checkNumber);
            funcResult.Should().BeTrue();
        }
    }
}
