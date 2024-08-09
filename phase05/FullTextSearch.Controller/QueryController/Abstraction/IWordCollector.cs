namespace FullTextSearch.Controller.QueryController.Abstraction;

public interface IWordCollector
{
    string Pattern { get; init; }
    char Sign { get; init; }
    IEnumerable<string> Collect(string text);
    IEnumerable<string> RemovePrefix(IEnumerable<string> collectedWords);
}