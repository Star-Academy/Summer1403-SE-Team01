using System.Text.RegularExpressions;
using FullTextSearch.Controller.QueryController.Abstraction;

namespace FullTextSearch.Controller.QueryController;

public class NoSignedWordCollector : IWordCollector
{
    public string Pattern { get; init; } = @"(\+""[^""]+""|\+\w+)|(-""[^""]+""|-\w+)|(""[^""]+""|\b\w+\b)";
    public char Sign { get; init; } = ' ';

    public IEnumerable<string> Collect(string text)
    {
        var regex = new Regex(Pattern);
        var matches = regex.Matches(text);

        return matches.Select(m => m.Value);
    }

    public IEnumerable<string> RemovePrefix(IEnumerable<string> collectedWords)
    {
        return collectedWords
            .Where(c => !c.StartsWith("+") && !c.StartsWith("-"))
            .Select(c => c.Trim('"').ToUpper());
    }
}