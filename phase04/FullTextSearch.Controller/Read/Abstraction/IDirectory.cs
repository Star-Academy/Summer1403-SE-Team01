namespace FullTextSearch.Controller.Read.Abstraction;

public interface IDirectory
{
    List<string> GetFiles(string dirPath);
}