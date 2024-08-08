using InvertedIndex.Abstraction.Read;

namespace InvertedIndex.Controller.Read;

public class TextFileReader : IFileReader
{
    public string ReadAsync(string path)
    {
        return  File.ReadAllText(path);
    }
}