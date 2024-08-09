using FullTextSearch.Controller.SearchController;
using FullTextSearch.Controller.SearchController.Abstraction;
using FullTextSearch.Core;

namespace FullTextSearch.Controller.ResultController.Abstraction;

public interface IResultBuilder
{
    void BuildDocumentsBySign(IEnumerable<ISearcher> searchers, Query query,
        Dictionary<string, IEnumerable<Document>> invertedIndex);

    void BuildDocuments(IEnumerable<IFilter> filters, Dictionary<string, IEnumerable<Document>> invertedIndex);
    Result GetResult();
}