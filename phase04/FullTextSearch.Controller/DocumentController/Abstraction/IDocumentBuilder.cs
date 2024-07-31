using FullTextSearch.Core;

namespace FullTextSearch.Controller.DocumentController.Abstraction;

public interface IDocumentBuilder
{

    public void BuildName(string name);
    public void BuildPath(string path);
    public void BuildText(string text);
    public void BuildWords();
    public Document GetDocument();
}