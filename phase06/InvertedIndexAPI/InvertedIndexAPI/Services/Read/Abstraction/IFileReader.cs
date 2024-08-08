namespace InvertedIndex.Abstraction.Read;

public interface IFileReader
{
    string ReadAsync(string path);
}