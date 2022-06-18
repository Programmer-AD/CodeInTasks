namespace CodeInTasks.Shared.Wrappers.Interfaces
{
    public interface IFileSystem
    {
        bool IsDirectoryExists(string path);
        bool IsFileExists(string path);

        void CreateDirectory(string path);

        FileStream OpenRead(string filePath);
        FileStream OpenWrite(string filePath);

        void DeleteFile(string path);
        void DeleteDirectory(string path, bool recursive);

        void CopyFile(string sourcePath, string destinationPath, bool overwrite);
    }
}
