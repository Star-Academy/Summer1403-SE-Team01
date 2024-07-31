using FullTextSearch.Core;

namespace FullTextSearch.Controller.QueryController.Abstraction;

public interface IQueryBuilder
{
    public void BuildText(string text);
    public void BuildWordsBySign(IEnumerable<char> signs);
    public Query GetQuery();
}