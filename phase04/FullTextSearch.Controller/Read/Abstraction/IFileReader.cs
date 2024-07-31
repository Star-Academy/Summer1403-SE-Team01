namespace InvertedIndex.Abstraction.Read;

public interface IFileReader
{
    public Task<string> ReadAsync(string path);
}