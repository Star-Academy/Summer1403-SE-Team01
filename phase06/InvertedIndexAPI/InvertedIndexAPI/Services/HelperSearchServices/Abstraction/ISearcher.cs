using FullTextSearch.Core;

namespace FullTextSearch.Controller.SearchController;

public interface ISearcher
{
    char Sign {get; init;}
    IEnumerable<Document> Search(Query query, Dictionary<string,IEnumerable<Document>> invertedIndex);
}