using System.Collections.Generic;
using CodeInTasks.Application.Filtration;

namespace CodeInTasks.Application.Tests.Filtration
{
    [TestFixture]
    public class FiltrationPipelineTests
    {
        private List<FiltrationAction<TestFilterDto, TestModel>> filtrationActions;

        private FiltrationPipeline<TestFilterDto, TestModel> filtrationPipeline;

        [SetUp]
        public void SetUp()
        {
            filtrationActions = new List<FiltrationAction<TestFilterDto, TestModel>>();

            filtrationPipeline = new FiltrationPipeline<TestFilterDto, TestModel>(filtrationActions);
        }

        //TODO: FiltrationPipelineTests
    }
}
