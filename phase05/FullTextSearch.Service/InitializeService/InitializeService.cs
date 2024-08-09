using FullTextSearch.Controller.DocumentController;
using FullTextSearch.Controller.DocumentController.Abstraction;
using FullTextSearch.Controller.InvertedIndexController;
using FullTextSearch.Controller.InvertedIndexController.Abstraction;
using FullTextSearch.Controller.Read.Abstraction;
using FullTextSearch.Core;
using FullTextSearch.Service.InitializeService.Abstraction;

namespace FullTextSearch.Service.InitializeService;

public class InitializeService : IInitializeService
{
    private readonly IFileReader _fileReader;
    private readonly IDocumentDirector _documentDirector;
    private readonly IInvertedIndexMapper _invertedIndexMapper;
    private readonly IDirectory _directory;
    private readonly IPath _path;

    public InitializeService(IFileReader fileReader, IDocumentDirector documentDirector, IInvertedIndexMapper invertedIndexMapper, IDirectory directory, IPath path)
    {
        _fileReader = fileReader;
        _documentDirector = documentDirector;
        _invertedIndexMapper = invertedIndexMapper;
        _directory = directory;
        _path = path;
    }

    public async Task<Dictionary<string, IEnumerable<Document>>> Initialize(string directoryPath)
    {
        List<Document> documents = new List<Document>();
        var paths = _directory.GetFiles(directoryPath);
        foreach (var p in paths)
        {
            IDocumentBuilder documentBuilder =
                new DocumentBuilder(new DocumentFormatter());
            _documentDirector.Construct(_path.GetFileName(p), p,await _fileReader.ReadAsync(p), documentBuilder);
            documents.Add(documentBuilder.GetDocument());
        }
        return _invertedIndexMapper.Map(documents);
    }
}