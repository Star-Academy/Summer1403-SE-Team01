using FullTextSearch.Core;

namespace FullTextSearch.Controller.SearchController;

public interface IUniversalSearch
{
    public IEnumerable<Document> GetUniversal(Dictionary<string,IEnumerable<Document>> dictionary);
}