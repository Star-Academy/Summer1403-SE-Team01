using FullTextSearch.Core;

namespace FullTextSearch.Controller.QueryController.Abstraction;

public interface IQueryBuilder
{
    void BuildText(string text);
    void BuildWordsBySign(IEnumerable<IWordCollector> collectors);
    Query GetQuery();
}