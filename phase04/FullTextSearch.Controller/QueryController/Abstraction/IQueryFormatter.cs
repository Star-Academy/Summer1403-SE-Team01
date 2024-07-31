namespace FullTextSearch.Controller.QueryController.Abstraction;

public interface IQueryFormatter
{
    public string ToUpper(string text);
    public IEnumerable<string> Split(string queryText, string regex);
    public IEnumerable<string> CollectBySign(IEnumerable<string> queryWords, char sign);
    public IEnumerable<string> RemovePrefix(IEnumerable<string> querySameSignWords);
}