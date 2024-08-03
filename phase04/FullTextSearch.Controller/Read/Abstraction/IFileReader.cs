namespace InvertedIndex.Abstraction.Read;

public interface IFileReader
{
    Task<string> ReadAsync(string path);
}