using FullTextSearch.Core;

namespace FullTextSearch.Controller.SearchController.Abstraction;

public interface ISearcher
{
    char Sign {get; init;}
    IEnumerable<Document> Search(Query query, Dictionary<string,IEnumerable<Document>> invertedIndex);
}