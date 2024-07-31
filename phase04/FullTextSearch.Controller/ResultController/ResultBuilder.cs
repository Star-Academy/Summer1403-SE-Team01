using FullTextSearch.Controller.SearchController;
using FullTextSearch.Core;
using InvertedIndex.Abstraction.Read;

namespace FullTextSearch.Controller.ResultController;

public class ResultBuilder : IResultBuilder
{

    private readonly Query _query;
    private readonly Result _result;
    private readonly IFiltererDriver _filtererDriver;
    private readonly IEnumerable<IFilterer> _filterers = new List<IFilterer>(){new NoSignedFilterer(), new PlusFilterer(), new MinusFilterer()};
    private readonly IEnumerable<ISearcher> _searchers = new List<ISearcher>(){new MinusSearcher(), new PlusSearcher(), new NoSignedSearcher()}; 
    private readonly ISearcherDriver _searcherDriver;
    private readonly Dictionary<string, IEnumerable<Document>> _invertedIndex;

    public ResultBuilder(IFiltererDriver filtererDriver, ISearcherDriver searcherDriver, Query query, Dictionary<string, IEnumerable<Document>> invertedIndex)
    {
        _result = new Result();
        _filtererDriver = filtererDriver;
        _searcherDriver = searcherDriver;
        _query = query;
        _invertedIndex = invertedIndex;
    }


    public void BuildDocumentsBySign()
    {
        _searcherDriver.DriveSearch(_searchers, _query, _result, _invertedIndex);
    }

    public void BuildDocuments()
    {
        _filtererDriver.DriveFilterer(_filterers, _result);      
    }
    
    public Result GetResult()
    {
        return _result;
    }
}