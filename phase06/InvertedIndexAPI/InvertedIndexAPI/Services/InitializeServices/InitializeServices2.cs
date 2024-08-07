using System.Reflection.Metadata;
using FullTextSearch.Controller.DocumentController;
using FullTextSearch.Controller.InvertedIndexController;
using InvertedIndex.Controller.Read;
using Directory = FullTextSearch.Controller.Read.Directory;
using Document = FullTextSearch.Core.Document;
using Path = FullTextSearch.Controller.Read.Path;

namespace FullTextSearch.Service.InitializeService;

public class InitializeServices2 : IInitializeServices2
{
    //private readonly IServiceProvider _serviceProvider;
    public static Dictionary<string, IEnumerable<Document>> InvertedIndex { get; private set; }

    public InitializeServices2(IServiceProvider serviceProvider)
    {
        //_serviceProvider = serviceProvider;
    }

    public async Task StartAsync_1()
    {
            var textFileReader = new TextFileReader();
            var documentDirector = new DocumentDirector();
            var invertedIndexMapper = new InvertedIndexMapper();
            var directory = new Directory();
            var path = new Path();

            var initializeService = new InitializeService(
                textFileReader, documentDirector, invertedIndexMapper, directory, path);
            
            var pathToData = "./Resources/EnglishData";
            InvertedIndex = await initializeService.Initialize(pathToData);
    }

    public Task StopAsync_1(CancellationToken cancellationToken) => Task.CompletedTask;
}