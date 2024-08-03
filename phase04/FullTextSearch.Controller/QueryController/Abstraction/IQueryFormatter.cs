namespace FullTextSearch.Controller.QueryController.Abstraction;

public interface IQueryFormatter
{
    string ToUpper(string text);
    IEnumerable<string> Split(string queryText, string regex);
    IEnumerable<string> CollectBySign(IEnumerable<string> queryWords, char sign);
    IEnumerable<string> RemovePrefix(IEnumerable<string> querySameSignWords);
}