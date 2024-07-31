using FullTextSearch.Controller.DocumentController;
using FullTextSearch.Controller.DocumentController.Abstraction;
using FullTextSearch.Controller.InvertedIndexController;
using FullTextSearch.Core;
using InvertedIndex.Abstraction.Read;

namespace FullTextSearch.Service.InitializeService;

public class InitializeService : IInitializeService
{
    private readonly IFileReader _fileReader;
    private readonly IDocumentDirector _documentDirector;

    public InitializeService(IFileReader fileReader, IDocumentDirector documentDirector)
    {
        _fileReader = fileReader;
        _documentDirector = documentDirector;
    }

    public async Task<Dictionary<string, IEnumerable<Document>>> Initialize(string directoryPath)
    {
        List<Document> documents = new List<Document>();
        var paths = Directory.GetFiles(directoryPath);
        foreach (var p in paths)
        {
            IDocumentBuilder documentBuilder =
                new DocumentBuilder(new DocumentFormatter());
            _documentDirector.Construct(Path.GetFileName(p), p,await _fileReader.ReadAsync(p), documentBuilder);
            documents.Add(documentBuilder.GetDocument());
        }

        InvertedIndexMapper mammper = new InvertedIndexMapper();
        return mammper.Map(documents);
    }
}