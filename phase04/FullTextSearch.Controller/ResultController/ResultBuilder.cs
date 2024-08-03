using FullTextSearch.Controller.ResultController.Abstraction;
using FullTextSearch.Controller.SearchController;
using FullTextSearch.Controller.SearchController.Abstraction;
using FullTextSearch.Core;

namespace FullTextSearch.Controller.ResultController;

public class ResultBuilder : IResultBuilder
{
    
    //private readonly Query _query;
    private readonly Result _result;
    private readonly ISearcherDriver _searcherDriver;
    private readonly IFilterDriver _filterDriver;
    //private readonly Dictionary<string, IEnumerable<Document>> _invertedIndex;

    public ResultBuilder(IFilterDriver filterDriver, ISearcherDriver searcherDriver)
    {
        _result = new Result();
        _filterDriver = filterDriver;
        _searcherDriver = searcherDriver;
        //_query = query;
        //_invertedIndex = invertedIndex;
    }
    
    public void BuildDocumentsBySign(IEnumerable<ISearcher> searchers, Query query, Dictionary<string, IEnumerable<Document>> invertedIndex)
    {
        //var searchers = new List<ISearcher>(){new MinusSearcher(), new PlusSearcher(), new NoSignedSearcher()};
        _searcherDriver.DriveSearch(searchers, query, _result, invertedIndex);
    }

    public void BuildDocuments(IEnumerable<IFilter> filters, Dictionary<string, IEnumerable<Document>> invertedIndex)
    {
        _result.documents = new UniversalSearch().GetUniversal(invertedIndex);
        //var filterers = new List<IFilter>(){new NoSignedFilter(), new PlusFilter(), new MinusFilter()};
        _filterDriver.DriveFilterer(filters, _result);      
    }
    
    public Result GetResult()
    {
        return _result;
    }
}