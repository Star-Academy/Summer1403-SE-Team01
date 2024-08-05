using FullTextSearch.Core;

namespace FullTextSearch.Controller.DocumentController.Abstraction;

public interface IDocumentBuilder
{

    void BuildName(string name);
    void BuildPath(string path);
    void BuildText(string text);
    void BuildWords();
    Document GetDocument();
}