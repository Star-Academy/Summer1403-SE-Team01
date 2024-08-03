using FullTextSearch.Core;

namespace FullTextSearch.Controller.SearchController.Abstraction;

public interface ISearcherDriver {
    public void DriveSearch(IEnumerable<ISearcher> searchers, Query query, Result result, Dictionary<string,IEnumerable<Document>> invertedIndex);
}