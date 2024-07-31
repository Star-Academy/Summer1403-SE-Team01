namespace FullTextSearch.Controller.DocumentController.Abstraction;

public interface IDocumentDirector
{
    public void Construct(string name, string path, string text, IDocumentBuilder documentBuilder);
}