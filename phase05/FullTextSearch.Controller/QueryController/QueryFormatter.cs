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
        return querySameSignWords.Select(word =>
        {
            if (word.StartsWith("+") || word.StartsWith("-"))
            {
                word = word.Substring(1);
            }
            if (word.StartsWith("\"") && word.EndsWith("\""))
            {
                word = word.Substring(1, word.Length - 2);
            }
            return word;
        });
    }
}