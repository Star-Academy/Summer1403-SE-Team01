using FullTextSearch.Core;

namespace FullTextSearch.Controller.SearchController;

public interface ISearcherDriver {
    public void DriveSearch(IEnumerable<ISearcher> searchers, Query query, Result result, Dictionary<string,IEnumerable<Document>> invertedIndex);
}