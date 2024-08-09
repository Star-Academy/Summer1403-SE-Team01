using FullTextSearch.Controller.SearchController.Abstraction;
using FullTextSearch.Core;

namespace FullTextSearch.Controller.SearchController;

public class SearcherDriver : ISearcherDriver
{
    public void DriveSearch(IEnumerable<ISearcher> searchers, Query query, Result result,
        Dictionary<string, IEnumerable<Document>> invertedIndex)
    {
        searchers.ToList()
            .ForEach(s => result.DocumentsBySign.Add(s.Sign, s.Search(query, invertedIndex)));
    }
}