using FullTextSearch.Core;

namespace FullTextSearch.Controller.SearchController.Abstraction;

public interface ISearcherDriver {
    void DriveSearch(IEnumerable<ISearcher> searchers, Query query, Result result, Dictionary<string,IEnumerable<Document>> invertedIndex);
}