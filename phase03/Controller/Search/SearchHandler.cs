using System.Data.SqlTypes;
using System.Linq.Expressions;
using System.Numerics;

public class SearchHandler // get query and result handler as property or as parameter
{
    public Dictionary<string, List<Document>> dictionary{ get; set;}
    public QueryHandler queryHandler;
    //public ResultHandler resultHandler;
    public List<char> signs = new List<char>{'+', '-', ' '};
    public List<ISearcher> searchables = new List<ISearcher>(){new MinusSearcher('-'), new PlusSearcher('+'), new NoSignedSearcher(' ')}; 
    public List<IFilterer> filterables = new List<IFilterer>(){new NoSignedFilterer(), new PlusFilterer(), new MinusFilterer()};

    public SearchHandler(Dictionary<string, List<Document>> dictionary)
    {
        this.dictionary = dictionary;
        queryHandler = new QueryHandler();
        //resultHandler = new ResultHandler();
    }

    public Search Search(string input)
    {
        var query = new Query(input);
        queryHandler.Prepare(query,new QueryProcessor(), signs);  // remove prepare

        var result = new Result();

        //var x = new List<ISearchable>(){new SignMinus('-'), new SignPlus('+'), new SignOrdinary(' ')};
        var driver = new SearcherDriver(searchables, query, result, dictionary);

        //resultHandler.Prepare(result);

        var y = new FiltererDirver(filterables, result);

        var search = new Search(query, result);    
        return search;
    }
}