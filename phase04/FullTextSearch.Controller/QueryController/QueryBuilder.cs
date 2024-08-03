using FullTextSearch.Controller.QueryController.Abstraction;
using FullTextSearch.Core;

namespace FullTextSearch.Controller.QueryController;

public class QueryBuilder : IQueryBuilder
{
    private readonly Query _query;
    private readonly IQueryFormatter _queryFormatter;
    
    public QueryBuilder(IQueryFormatter queryFormatter)
    {
        _query = new Query();
        _queryFormatter = queryFormatter;
    }

    public void BuildText(string text)
    {
        _query.Text = text;
    }

    public void BuildWordsBySign(IEnumerable<char> signs)
    {
        var splittedText = _queryFormatter.Split(_queryFormatter.ToUpper(_query.Text), " "); // [^A-Z]+

        foreach (var sign in signs)
        {
            var queryWords = splittedText as string[] ?? splittedText.ToArray();
            var texts = _queryFormatter.CollectBySign(queryWords, sign);
            splittedText = queryWords.Except(texts);
            _query.WordsBySign.Add(sign, _queryFormatter.RemovePrefix(texts));
        }
        _query.WordsBySign[' '] = splittedText;
    }

    public Query GetQuery()
    {
        return _query;
    }
}