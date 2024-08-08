using FullTextSearch.Controller.DocumentController;
using FullTextSearch.Controller.InvertedIndexController;
using InvertedIndex.Controller.Read;
using Directory = FullTextSearch.Controller.Read.Directory;
using Document = FullTextSearch.Core.Document;
using Path = FullTextSearch.Controller.Read.Path;

namespace FullTextSearch.Service.InitializeService
{
    public class InitializeServicesDriver : IInitializeServices2
    {
        private readonly IConfiguration _configuration;

        public static Dictionary<string, IEnumerable<Document>> InvertedIndex { get; set; }

        public InitializeServicesDriver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Drive()
        {
            var textFileReader = new TextFileReader();
            var documentDirector = new DocumentDirector();
            var invertedIndexMapper = new InvertedIndexMapper();
            var directory = new Directory();
            var path = new Path();

            var initializeService = new InitializeService(
                textFileReader, documentDirector, invertedIndexMapper, directory, path);
            
            var pathToData = _configuration["FilePaths:DataPath"];
            InvertedIndex = initializeService.Initialize(pathToData);
        }
    }
}