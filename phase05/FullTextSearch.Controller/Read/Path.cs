using FullTextSearch.Controller.Read.Abstraction;

namespace FullTextSearch.Controller.Read;

public class Path : IPath
{
    public string GetFileName(string path)
    {
        return System.IO.Path.GetFileName(path);
    }
}