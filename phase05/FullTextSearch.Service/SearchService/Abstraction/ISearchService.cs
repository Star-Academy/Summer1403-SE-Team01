using FullTextSearch.Core;

namespace FullTextSearch.Service.SearchService.Abstraction;

public interface ISearchService
{
    Result Search(string input, Dictionary<string, IEnumerable<Document>> invertedIndex);
}