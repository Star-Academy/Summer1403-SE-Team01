namespace InvertedIndex.Controller.Document;

public interface IDocumentFormatter
{
   string ToUpper(string text);
   IEnumerable<string> Split(string queryText, string regex); 
}