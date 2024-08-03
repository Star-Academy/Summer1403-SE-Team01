namespace FullTextSearch.Controller.Read.Abstraction;

public interface IDirectory
{
    public List<string> GetFiles(string dirPath);
}