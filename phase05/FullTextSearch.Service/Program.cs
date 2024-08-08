using System.Net.Security;
using Castle.Core.Logging;
using FullTextSearch.Controller.DocumentController;
using FullTextSearch.Controller.InvertedIndexController;
using FullTextSearch.Controller.QueryController;
using FullTextSearch.Controller.ResultController;
using FullTextSearch.Controller.SearchController;
using FullTextSearch.Controller.TextFormatter;
using FullTextSearch.Core;
using FullTextSearch.Service.InitializeService;
using FullTextSearch.Service.SearchService;
using InvertedIndex.Controller.Read;
using Directory = FullTextSearch.Controller.Read.Directory;
using Path = FullTextSearch.Controller.Read.Path;


class Program
{
    static async Task Main(string[] args)
    {
        // ./Resources/EnglishData
        var path = Console.ReadLine();
        var initializeService = new InitializeService(new TextFileReader(), new DocumentDirector(),
            new InvertedIndexMapper(), new Directory(), new Path());
        
        var invertedIndex = await initializeService.Initialize(path);
        
        var searchService = new SearchService(new QueryBuilder(new QueryFormatter(), new TextFormatter()),
            new ResultBuilder(new FilterDriver(), new SearcherDriver()));//
        
        string input;
        while (!(input = Console.ReadLine()).Equals("end"))
        {
            searchService.Search(input, invertedIndex).documents.ToList().ForEach(Console.WriteLine);
        }
    }
}//+cat -reza mohammad