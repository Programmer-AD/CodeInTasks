using CodeInTasks.Shared.Wrappers.Interfaces;

namespace CodeInTasks.Builder.Services
{
    internal class NamingService : INamingService
    {
        private readonly IFileSystem fileSystem;

        public NamingService(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        public string GetBuilderName()
        {
            if (!TryGetExistingBuilderName(out var builderName))
            {
                builderName = GetNewBuilderName();

                SaveBuilderName(builderName);
            }

            return builderName;
        }

        private bool TryGetExistingBuilderName(out string builderName)
        {
            if (fileSystem.IsFileExists(BuilderConstants.NameFilePath))
            {
                using var stream = fileSystem.OpenRead(BuilderConstants.NameFilePath);
                using var reader = new StreamReader(stream);

                builderName = reader.ReadToEnd();

                return !string.IsNullOrEmpty(builderName);
            }
            else
            {
                builderName = string.Empty;
                return false;
            }
        }

        private static string GetNewBuilderName()
        {
            var guid = Guid.NewGuid();
            var guidBytes = guid.ToByteArray();

            var base64 = Convert.ToBase64String(guidBytes);

            return base64;
        }

        private void SaveBuilderName(string builderName)
        {
            EnsureContainerDirectoryExists(BuilderConstants.NameFilePath);

            using var stream = fileSystem.OpenWrite(BuilderConstants.NameFilePath);
            using var writer = new StreamWriter(stream);

            writer.Write(builderName);
        }

        private void EnsureContainerDirectoryExists(string filePath)
        {
            var directoryPath = Path.GetDirectoryName(filePath);

            if (!fileSystem.IsDirectoryExists(directoryPath))
            {
                fileSystem.CreateDirectory(directoryPath);
            }
        }
    }
}
