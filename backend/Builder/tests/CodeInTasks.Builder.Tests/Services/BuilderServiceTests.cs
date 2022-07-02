using CodeInTasks.Builder.Services;
using CodeInTasks.Shared.Queues.Abstractions.Interfaces;
using CodeInTasks.Shared.Queues.Abstractions.Models;
using CodeInTasks.Shared.Queues.Messages;
using Microsoft.Extensions.Logging;

namespace CodeInTasks.Builder.Tests.Services
{
    [TestFixture]
    public class BuilderServiceTests
    {
        private const int timeoutMilliseconds = 5;
        private static readonly Message<SolutionCheckQueueMessage> message = new("some_id", new());

        private CancellationTokenSource cancellationSource;

        private Mock<INamingService> namingServiceMock;
        private Mock<IMessageQueue<SolutionCheckQueueMessage>> messageQueueMock;
        private Mock<IRuntime> runtimeMock;
        private Mock<ILogger<BuilderService>> loggerMock;

        private BuilderService builderService;

        [SetUp]
        public void SetUp()
        {
            cancellationSource = new();

            namingServiceMock = new();
            messageQueueMock = new();
            runtimeMock = new();
            loggerMock = new();

            builderService = new(
                namingServiceMock.Object,
                messageQueueMock.Object,
                runtimeMock.Object,
                loggerMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            cancellationSource.Dispose();
        }

        [Test]
        public async Task RunAsync_WhenCancellationRequested_StopsWithoutException()
        {
            const int timeoutReserve = 5;

            var timeoutTask = Task.Delay(timeoutMilliseconds + timeoutReserve)
                .ContinueWith(task => throw new TimeoutException());


            var runTask = Call_BuilderService_RunAsync();


            var firstCompleted = await Task.WhenAny(runTask, timeoutTask);
            firstCompleted.IsCompletedSuccessfully.Should().BeTrue();
        }

        [Test]
        public async Task RunAsync_CallGetBuilderNameOnce()
        {
            await Call_BuilderService_RunAsync();


            namingServiceMock.Verify(x => x.GetBuilderName(), Times.Once);
        }

        [Test]
        public async Task RunAsync_WhenMessageIsNull_NotCallHandleAsync()
        {
            SetGetMessageAsyncResult(null);


            await Call_BuilderService_RunAsync();


            runtimeMock.Verify(x => x.HandleAsync(It.IsAny<SolutionCheckQueueMessage>()), Times.Never);
        }

        [Test]
        public async Task RunAsync_WhenMessageIsNull_NotCallAcknowledgeAsync()
        {
            SetGetMessageAsyncResult(null);


            await Call_BuilderService_RunAsync();


            messageQueueMock.Verify(x => x.AcknowledgeAsync(It.IsAny<Message<SolutionCheckQueueMessage>>()), Times.Never);
        }

        [Test]
        public async Task RunAsync_WhenHaveMessage_CallHandleAsync()
        {
            SetGetMessageAsyncResult(message);


            await Call_BuilderService_RunAsync();


            runtimeMock.Verify(x => x.HandleAsync(It.IsAny<SolutionCheckQueueMessage>()));
        }

        [Test]
        public async Task RunAsync_WhenHaveMessage_CallAcknowledgeAsync()
        {
            SetGetMessageAsyncResult(message);


            await Call_BuilderService_RunAsync();


            messageQueueMock.Verify(x => x.AcknowledgeAsync(It.IsAny<Message<SolutionCheckQueueMessage>>()));
        }

        [Test]
        public async Task RunAsync_WhenHandleAsyncThrowException_NotCallAcknowledgeAsync()
        {
            runtimeMock
                .Setup(x => x.HandleAsync(It.IsAny<SolutionCheckQueueMessage>()))
                .Throws<Exception>();


            await CallHelpers.ForceCallAsync<Exception>(() => Call_BuilderService_RunAsync());


            messageQueueMock.Verify(x => x.AcknowledgeAsync(It.IsAny<Message<SolutionCheckQueueMessage>>()), Times.Never);
        }

        private Task Call_BuilderService_RunAsync()
        {
            cancellationSource.CancelAfter(timeoutMilliseconds);

            return builderService.RunAsync(cancellationSource.Token);
        }

        private void SetGetMessageAsyncResult(Message<SolutionCheckQueueMessage> message)
        {
            messageQueueMock
                .Setup(x => x.GetMessageAsync(It.IsAny<string>()))
                .ReturnsAsync(message);
        }
    }
}
