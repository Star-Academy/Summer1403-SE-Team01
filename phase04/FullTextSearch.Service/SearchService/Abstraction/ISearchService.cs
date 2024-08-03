using FullTextSearch.Core;

namespace FullTextSearch.Service.SearchService;

public interface ISearchService
{
    Result Search(string input, Dictionary<string, IEnumerable<Document>> invertedIndex);
}