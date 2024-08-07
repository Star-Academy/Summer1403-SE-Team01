namespace FullTextSearch.Controller.DocumentController.Abstraction;

public interface IDocumentDirector
{
    void Construct(string name, string path, string text, IDocumentBuilder documentBuilder);
}