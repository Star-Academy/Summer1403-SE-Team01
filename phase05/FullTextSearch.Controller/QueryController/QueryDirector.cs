using FullTextSearch.Controller.QueryController.Abstraction;

namespace FullTextSearch.Controller.QueryController;

public class QueryDirector
{
    public void Construct(string text, IEnumerable<char> signs, IQueryBuilder queryBuilder)
    {
        queryBuilder.BuildText(text);
        queryBuilder.BuildWordsBySign(signs);
    }
}