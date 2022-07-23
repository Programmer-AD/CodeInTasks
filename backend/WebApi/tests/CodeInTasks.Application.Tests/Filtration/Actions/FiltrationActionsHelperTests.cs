using CodeInTasks.Application.Filtration.Actions;
using CodeInTasks.Application.Filtration;
using System.Linq.Expressions;

// Let correct TFilterDto and TEntity be int
using TFilterDto = System.Int32;
using TEntity = System.Int32;

// Let incorrect TFilterDto and TEntity be float
using TOtherFilterDto = System.Single;
using TOtherEntity = System.Single;


namespace CodeInTasks.Application.Tests.Filtration.Actions
{
    [TestFixture]
    public class FiltrationActionsHelperTests
    {
        [Test]
        public void GetActions_GetsActionsWithCorrectSignature()
        {
            var containerType = typeof(TestActionContainer);


            var actions = FiltrationActionsHelper.GetActions<TFilterDto, TEntity>(containerType);


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
            public static void CorrectMethod(TFilterDto filter, FiltrationPipelineResult<TEntity> result) { }
            public static void OtherCorrectMethod(TFilterDto otherFilterName, FiltrationPipelineResult<TEntity> otherResultName) { }

            private static void HiddenMethod(TFilterDto filter, FiltrationPipelineResult<TEntity> result) { }
            public void NonStaticMethod(TFilterDto filter, FiltrationPipelineResult<TEntity> result) { }

            public static void WrongArgumentMethod1(TOtherFilterDto otherFilter, FiltrationPipelineResult<TOtherEntity> result) { }
            public static void WrongArgumentMethod2(TFilterDto filter, TEntity result) { }
            public static void LessArgumentMethod(TFilterDto filter) { }
            public static void MoreArgumentMethod(TFilterDto filter, FiltrationPipelineResult<TEntity> result, string more) { }
            public static void MoreArgumentDefaultMethod(TFilterDto filter, FiltrationPipelineResult<TEntity> result, string more = "12") { }
        }
    }
}
