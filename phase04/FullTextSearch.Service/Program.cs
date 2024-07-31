using FullTextSearch.Controller.DocumentController;
using FullTextSearch.Service.InitializeService;
using FullTextSearch.Service.SearchService;
using InvertedIndex.Controller.Read;


class Program
{
    static async Task Main(string[] args)
    {
        var path = "./Resources/EnglishData";
        InitializeService initializeService = new InitializeService(new TextFileReader(), new DocumentDirector());
        var map = await initializeService.Initialize(path);
        SearchService searchService = new SearchService();
        var input = "cat -demand!";
        
        foreach (var val in searchService.Search(input, map).documents)
        {
            Console.WriteLine(val);
        }
    }
}