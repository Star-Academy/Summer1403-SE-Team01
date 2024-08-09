using FullTextSearch.Controller.Read.Abstraction;

namespace FullTextSearch.Controller.Read;

public class TextFileReader : IFileReader
{
    public async Task<string> ReadAsync(string path)
    {
        return await File.ReadAllTextAsync(path);
    }
}