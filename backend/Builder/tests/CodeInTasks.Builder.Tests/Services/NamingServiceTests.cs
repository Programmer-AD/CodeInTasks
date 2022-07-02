using CodeInTasks.Builder.Services;
using CodeInTasks.Shared.Wrappers.Interfaces;

namespace CodeInTasks.Builder.Tests.Services
{
    [TestFixture]
    public class NamingServiceTests
    {
        private const string existingName = "some_name";

        private string tempSaveFileName;

        private Mock<IFileSystem> fileSystemMock;
        private NamingService namingService;

        [SetUp]
        public void SetUp()
        {
            tempSaveFileName = Path.GetTempFileName();

            fileSystemMock = new();

            namingService = new(fileSystemMock.Object);

            SetupFileStreams();
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(tempSaveFileName))
            {
                File.Delete(tempSaveFileName);
            }
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
        public void GetBuilderName_WhenSaveDirectoryNotExists_CreatesIt()
        {
            SetNameExists(false);
            SetSaveDirectoryExists(false);


            var name = namingService.GetBuilderName();


            var savedName = GetSavedName();
            savedName.Should().Be(name);
        }

        [Test]
        public void GetBuilderName_WhenNameNotExist_SaveNewName()
        {
            SetNameExists(false);
            SetSaveDirectoryExists(false);


            var _ = namingService.GetBuilderName();


            fileSystemMock.Verify(x => x.CreateDirectory(It.IsAny<string>()));
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
                .Returns(() => File.OpenRead(tempSaveFileName));

            fileSystemMock
                .Setup(x => x.OpenWrite(It.IsAny<string>()))
                .Returns(() => File.OpenWrite(tempSaveFileName));
        }

        private void SetNameExists(bool exists)
        {
            fileSystemMock
                .Setup(x => x.IsFileExists(It.IsAny<string>()))
                .Returns(exists);
        }

        private void SetExistingName(string name)
        {
            using var writer = new StreamWriter(tempSaveFileName);

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
            using var reader = new StreamReader(tempSaveFileName);

            var result = reader.ReadToEnd();

            return result;
        }
    }
}
