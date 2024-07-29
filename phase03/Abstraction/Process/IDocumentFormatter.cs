public interface IDocumentFormatter
{
    public string ToUpper(string text);
    public List<string> Split(string text);
    public string RemoveExtraSpace(string str);
}