using System.Text.RegularExpressions;
public class DocumentFormatter : IDocumentFormatter{
    private const string RegexPattern = "\\s+";
    public string ToUpper(string text)
    {
        var uppered = text.ToUpper();
        return uppered;
    }
    
    public List<string> Split(string text)
    {

        var splittedText = text.Split(" ").ToList();
        return splittedText;
    }
    
    public string RemoveExtraSpace(string str)
    {
        return Regex.Replace(str, RegexPattern, " ");
    }
}