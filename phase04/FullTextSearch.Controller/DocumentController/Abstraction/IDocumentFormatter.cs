namespace InvertedIndex.Controller.Document;

public interface IDocumentFormatter
{
    public string ToUpper(string text);
    public IEnumerable<string> Split(string queryText, string regex);
}