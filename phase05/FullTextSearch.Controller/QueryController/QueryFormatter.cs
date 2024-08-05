using System.Text.RegularExpressions;
using FullTextSearch.Controller.QueryController.Abstraction;

namespace FullTextSearch.Controller.QueryController;

public class QueryFormatter : IQueryFormatter
{
    public string ToUpper(string text)
    {
        return text.ToUpper();
    }

    public IEnumerable<string> Split(string queryText, string regex)
    {
        return Regex.Split(queryText, regex);
    }

    public IEnumerable<string> CollectBySign(IEnumerable<string> queryWords, char sign)
    {
        return queryWords.Where(w => w[0].Equals(sign));
    }

    public IEnumerable<string> RemovePrefix(IEnumerable<string> querySameSignWords)
    {
        return querySameSignWords.Select(w=>w.Substring(1, w.Length - 1));
    }
}