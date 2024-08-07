namespace FullTextSearch.Service.SearchService;

public class InitializeService
{
    private readonly IServiceProvider _serviceProvider;
    public static InvertedIndex InvertedIndex { get; private set; }

    public InitializeService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var textFileReader = new TextFileReader();
            var documentDirector = new DocumentDirector();
            var invertedIndexMapper = new InvertedIndexMapper();
            var directory = new Directory();
            var path = new Path();

            var initializeService = new FullTextSearch.Service.InitializeService.InitializeService(
                textFileReader, documentDirector, invertedIndexMapper, directory, path);

            // Replace with the actual path or make it configurable
            var pathToData = "./Resources/EnglishData";
            InvertedIndex = await initializeService.Initialize(pathToData);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}