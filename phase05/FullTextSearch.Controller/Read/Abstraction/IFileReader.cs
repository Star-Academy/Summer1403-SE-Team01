namespace FullTextSearch.Controller.Read.Abstraction;

public interface IFileReader
{
    Task<string> ReadAsync(string path);
}