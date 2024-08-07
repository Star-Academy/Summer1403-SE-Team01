using FullTextSearch.Controller.DocumentController.Abstraction;

namespace FullTextSearch.Controller.DocumentController;

public class DocumentDirector : IDocumentDirector
{
    public void Construct(string name, string path, string text, IDocumentBuilder documentBuilder)
    {
        documentBuilder.BuildName(name);
        documentBuilder.BuildPath(path);
        documentBuilder.BuildText(text);
        documentBuilder.DeleteExtraSpace();
        documentBuilder.BuildWords();
    }

}