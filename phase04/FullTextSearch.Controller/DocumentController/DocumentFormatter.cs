using System.Text.RegularExpressions;
using InvertedIndex.Controller.Document;

namespace FullTextSearch.Controller.DocumentController;
public class DocumentFormatter : IDocumentFormatter{

    public string ToUpper(string text)
    {
        return text.ToUpper();
    }

    public IEnumerable<string> Split(string queryText, string regex)
    {
        return Regex.Split(queryText, regex);
    }
}