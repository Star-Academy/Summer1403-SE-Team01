using InvertedIndex.Abstraction.Read;

namespace InvertedIndex.Controller.Read;

public class TextFileReader : IFileReader
{
    public async Task<string> ReadAsync(string path)
    {
        return await File.ReadAllTextAsync(path);
    }
}