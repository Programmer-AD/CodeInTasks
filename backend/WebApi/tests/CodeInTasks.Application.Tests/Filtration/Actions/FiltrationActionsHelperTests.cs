using System.Linq.Expressions;
using CodeInTasks.Application.Filtration;
using CodeInTasks.Application.Filtration.Actions;
using TEntity = System.Int32;
// Let correct TFilterModel and TEntity be int
using TFilterModel = System.Int32;
using TOtherEntity = System.Single;
// Let incorrect TFilterModel and TEntity be float
using TOtherFilterModel = System.Single;


namespace CodeInTasks.Application.Tests.Filtration.Actions
{
    [TestFixture]
    public class FiltrationActionsHelperTests
    {
        [Test]
        public void GetActions_GetsActionsWithCorrectSignature()
        {
            var containerType = typeof(TestActionContainer);


            var actions = FiltrationActionsHelper.GetActions<TFilterModel, TEntity>(containerType);


            actions.Should().HaveCount(2)
                .And.Contain(HasMethodName(nameof(TestActionContainer.CorrectMethod)))
                .And.Contain(HasMethodName(nameof(TestActionContainer.OtherCorrectMethod)));
        }

        private static Expression<Func<FiltrationAction<int, int>, bool>> HasMethodName(string expectedName)
        {
            return x => x.Method.Name == expectedName;
        }

        private class TestActionContainer
        {
            public static void CorrectMethod(TFilterModel filter, FiltrationPipelineResult<TEntity> result) { }
            public static void OtherCorrectMethod(TFilterModel otherFilterName, FiltrationPipelineResult<TEntity> otherResultName) { }

            private static void HiddenMethod(TFilterModel filter, FiltrationPipelineResult<TEntity> result) { }
            public void NonStaticMethod(TFilterModel filter, FiltrationPipelineResult<TEntity> result) { }

            public static void WrongArgumentMethod1(TOtherFilterModel otherFilter, FiltrationPipelineResult<TOtherEntity> result) { }
            public static void WrongArgumentMethod2(TFilterModel filter, TEntity result) { }
            public static void LessArgumentMethod(TFilterModel filter) { }
            public static void MoreArgumentMethod(TFilterModel filter, FiltrationPipelineResult<TEntity> result, string more) { }
            public static void MoreArgumentDefaultMethod(TFilterModel filter, FiltrationPipelineResult<TEntity> result, string more = "12") { }
        }
    }
}
