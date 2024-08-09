using FullTextSearch.Controller.DocumentController;
using FullTextSearch.Controller.InvertedIndexController;
using FullTextSearch.Controller.Read;
using Directory = FullTextSearch.Controller.Read.Directory;
using Path = FullTextSearch.Controller.Read.Path;


namespace FullTextSearch.Service;

class Program
{
    static async Task Main(string[] args)
    {
        // ./Resources/EnglishData
        var path = Console.ReadLine();
        var initializeService = new InitializeService.InitializeService(new TextFileReader(), new DocumentDirector(),
            new InvertedIndexMapper(), new Directory(), new Path());
        var invertedIndex = await initializeService.Initialize(path);
        var searchService = new SearchService.SearchService();
        string input;
        while (!(input = Console.ReadLine()).Equals("end"))
        {
            searchService.Search(input, invertedIndex).Documents.ToList().ForEach(Console.WriteLine);
        }
    }
    
}
