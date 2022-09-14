using CodeInTasks.Builder.Services;
using CodeInTasks.Shared.Wrappers.Interfaces;

namespace CodeInTasks.Builder.Tests.Services
{
    [TestFixture]
    public class NamingServiceTests
    {
        private const int bufferSize = 256;
        private const string existingName = "some_name";

        private byte[] buffer;

        private Mock<IFileSystem> fileSystemMock;
        private NamingService namingService;

        [SetUp]
        public void SetUp()
        {
            buffer = new byte[bufferSize];

            fileSystemMock = new();

            namingService = new(fileSystemMock.Object);

            SetupFileStreams();
        }

        [Test]
        public void GetBuilderName_WhenNameNotExist_ReturnNewName()
        {
            SetNameExists(false);
            SetSaveDirectoryExists(true);


            var name = namingService.GetBuilderName();


            name.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void GetBuilderName_WhenNameNotExist_MakeDifferentNewNames()
        {
            SetNameExists(false);
            SetSaveDirectoryExists(true);


            var name = namingService.GetBuilderName();
            var otherName = namingService.GetBuilderName();


            name.Should().NotBe(otherName);
        }

        [Test]
        public void GetBuilderName_WhenNameNotExist_SaveNewName()
        {
            SetNameExists(false);
            SetSaveDirectoryExists(false);


            var name = namingService.GetBuilderName();


            var savedName = GetSavedName();
            savedName.Should().Be(name);
        }

        [Test]
        public void GetBuilderName_WhenSaveDirectoryNotExists_CreatesIt()
        {
            SetNameExists(false);
            SetSaveDirectoryExists(false);


            var _ = namingService.GetBuilderName();


            fileSystemMock.Verify(x => x.CreateDirectory(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void GetBuilderName_WhenNameExists_ReturnExistingName()
        {
            SetNameExists(true);
            SetExistingName(existingName);


            var name = namingService.GetBuilderName();


            name.Should().Be(existingName);
        }

        private void SetupFileStreams()
        {
            fileSystemMock
                .Setup(x => x.OpenRead(It.IsAny<string>()))
                .Returns(GetBufferReadStream);

            fileSystemMock
                .Setup(x => x.OpenWrite(It.IsAny<string>()))
                .Returns(GetBufferWriteStream);
        }

        private void SetNameExists(bool exists)
        {
            fileSystemMock
                .Setup(x => x.IsFileExists(It.IsAny<string>()))
                .Returns(exists);
        }

        private void SetExistingName(string name)
        {
            using var stream = GetBufferWriteStream();
            using var writer = new StreamWriter(stream);

            writer.Write(name);
        }

        private void SetSaveDirectoryExists(bool exists)
        {
            fileSystemMock
                .Setup(x => x.IsDirectoryExists(It.IsAny<string>()))
                .Returns(exists);
        }

        private string GetSavedName()
        {
            using var stream = GetBufferReadStream();
            using var reader = new StreamReader(stream);

            var result = reader.ReadToEnd();

            return result;
        }

        private Stream GetBufferReadStream()
        {
            return new MemoryStream(buffer, 0, GetUsedBufferLength());
        }

        private Stream GetBufferWriteStream()
        {
            return new MemoryStream(buffer);
        }

        private int GetUsedBufferLength()
        {
            var result = Array.FindIndex(buffer, x => x is 0);
            return result;
        }
    }
}
