using FullTextSearch.Controller.Read.Abstraction;

namespace FullTextSearch.Controller.Read;

public class Directory : IDirectory
{
    public List<string> GetFiles(string dirPath)
    {
        return System.IO.Directory.GetFiles(dirPath).ToList();
    }
}