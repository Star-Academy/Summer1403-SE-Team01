using FullTextSearch.Controller.QueryController.Abstraction;
using FullTextSearch.Core;

namespace FullTextSearch.Controller.QueryController;

public class QueryBuilder : IQueryBuilder
{
    private readonly IWordCollectorDriver _wordCollectorDriver;
    private readonly Query _query;
    
    public QueryBuilder(IWordCollectorDriver wordCollectorDriver)
    {
        _wordCollectorDriver = wordCollectorDriver;
        _query = new Query();
    }

    public void BuildText(string text)
    {
        _query.Text = text;
    }

    public void BuildWordsBySign(IEnumerable<IWordCollector> collectors)
    {
        _wordCollectorDriver.DriveCollect(collectors, _query);
    }
    
    public Query GetQuery()
    {
        return _query;
    }
}
