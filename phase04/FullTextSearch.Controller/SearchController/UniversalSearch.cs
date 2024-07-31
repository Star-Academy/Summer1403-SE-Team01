
using FullTextSearch.Core;

namespace FullTextSearch.Controller.SearchController;

public class UniversalSearch : IUniversalSearch
{
    public IEnumerable<Document> GetUniversal(Dictionary<string, IEnumerable<Document>> invertedIndex)
    {
        var universalList = invertedIndex.Values.SelectMany(d => d).Distinct();
        return universalList;
    }
}