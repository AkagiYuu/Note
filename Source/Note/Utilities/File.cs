using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Note.Utilities;

public static class File
{
    public static async Task Save(string Path, string Content)
    {
        var file = await StorageFile.GetFileFromPathAsync(Path);

        await FileIO.WriteTextAsync(file, Content);
    }

    public static async Task Save(StorageFile file, string Content) => await FileIO.WriteTextAsync(file, Content);

    public static async Task<string> Open(StorageFile file) => await FileIO.ReadTextAsync(file);
}
