using System.Data.SqlTypes;
using System.Linq.Expressions;

public class SearchHandler 
{
    public Dictionary<string, List<Document>> dictionary{ get; set;}//inverted index
    public QueryHandler queryHandler;
    public ResultHandler resultHandler;

    public SearchHandler(Dictionary<string, List<Document>> dictionary)
    {
        this.dictionary = dictionary;
        queryHandler = new QueryHandler();
        resultHandler = new ResultHandler();
    }

    public Search Search(string input)
    {
        var query = new Query(input);
        queryHandler.Prepare(query,new QueryEditor(), new List<char>{'+', '-', ' '});//use property for search

        var result = new Result();

        var x = new List<ISignable>(){new SignMinus('-'), new SignPlus('+'), new SignOrdinary(' ')};
        var driver = new Driver(x, query, result, dictionary);

        //resultHandler.Prepare(result);

        var y = new ResultDriver(new List<IResultable>(){new ResultOrdinary(), new ResultPlus(), new ResultMinus()}, result);

        var search = new Search(query, result);    
        return search;
    }
}