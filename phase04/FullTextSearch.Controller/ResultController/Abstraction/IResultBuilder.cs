using FullTextSearch.Controller.SearchController;
using FullTextSearch.Core;

namespace FullTextSearch.Controller.ResultController.Abstraction;

public interface IResultBuilder
{
    public void BuildDocumentsBySign(IEnumerable<ISearcher> searchers, Query query,
        Dictionary<string, IEnumerable<Document>> invertedIndex);

    public void BuildDocuments(IEnumerable<IFilter> filters, Dictionary<string, IEnumerable<Document>> invertedIndex);
    public Result GetResult();
}