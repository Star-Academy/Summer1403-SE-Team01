using FullTextSearch.Core;

namespace FullTextSearch.Controller.SearchController;

public interface ISearcher
{
    public char Sign {get; init;}
    public IEnumerable<Document> Search(Query query, Dictionary<string,IEnumerable<Document>> invertedIndex);
}