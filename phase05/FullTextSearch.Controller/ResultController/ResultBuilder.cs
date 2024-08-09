using FullTextSearch.Controller.ResultController.Abstraction;
using FullTextSearch.Controller.SearchController;
using FullTextSearch.Controller.SearchController.Abstraction;
using FullTextSearch.Core;

namespace FullTextSearch.Controller.ResultController;

public class ResultBuilder : IResultBuilder
{
    
    private readonly Result _result;
    private readonly ISearcherDriver _searcherDriver;
    private readonly IFilterDriver _filterDriver;

    public ResultBuilder(IFilterDriver filterDriver, ISearcherDriver searcherDriver)
    {
        _result = new Result();
        _filterDriver = filterDriver ?? throw new ArgumentNullException(nameof(filterDriver));
        _searcherDriver = searcherDriver ?? throw new ArgumentNullException(nameof(searcherDriver));
    }
    
    public void BuildDocumentsBySign(IEnumerable<ISearcher> searchers, Query query, 
        Dictionary<string, IEnumerable<Document>> invertedIndex)
    {
        _searcherDriver.DriveSearch(searchers, query, _result, invertedIndex);
    }

    public void BuildDocuments(IEnumerable<IFilter> filters, 
        Dictionary<string, IEnumerable<Document>> invertedIndex)
    {
        _result.Documents = new UniversalSearch().GetUniversal(invertedIndex);
        _filterDriver.DriveFilterer(filters, _result);      
    }
    
    public Result GetResult()
    {
        return _result;
    }
}