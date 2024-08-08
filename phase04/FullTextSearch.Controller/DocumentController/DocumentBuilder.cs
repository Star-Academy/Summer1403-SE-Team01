using FullTextSearch.Controller.DocumentController.Abstraction;
using FullTextSearch.Core;
using InvertedIndex.Controller.Document;

namespace FullTextSearch.Controller.DocumentController;

public class DocumentBuilder : IDocumentBuilder
{
    private readonly Document _document;
    private readonly IDocumentFormatter _documentFormatter;

    public DocumentBuilder(IDocumentFormatter documentFormatter)
    {
        _documentFormatter = documentFormatter ?? throw new ArgumentNullException(nameof(documentFormatter));
        _document = new Document();
    }
    
    public void BuildName(string name)
    {
        _document.Name = name;
    }
    
    public void BuildPath(string path)
    {
        _document.Path = path;
    }
    
    public void BuildText(string text)
    {
        _document.Text = text;
    }

    public void BuildWords()
    {
        _document.Words = _documentFormatter.Split(_documentFormatter.ToUpper(_document.Text), " ");
    }

    public Document GetDocument()
    {
        return _document;
    }
}