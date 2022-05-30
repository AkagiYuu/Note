using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;

namespace Note.Utilities;

public struct FileInfo
{
    public string FullName { get; }
    public string Name => Path.GetFileName(FullName);

    public FileInfo(string Path) => FullName = Path;
}

public static class File
{
    public static async Task Save(string Path, string Content)
    {
        var file = await StorageFile.GetFileFromPathAsync(Path);

        await FileIO.WriteTextAsync(file, Content);
    }

    public static async Task Save(StorageFile file, string Content) => await FileIO.WriteTextAsync(file, Content);

    public static async Task<string> Open(StorageFile file) => await FileIO.ReadTextAsync(file);

    public static async Task<string> Open(string Path) => await System.IO.File.ReadAllTextAsync(Path);
}