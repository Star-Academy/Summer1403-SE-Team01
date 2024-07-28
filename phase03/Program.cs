using System;
class Program
{
    static async Task Main(string[] args)
    {
        string folderPath = Console.ReadLine();// ./Resources/EnglishData
        try
        {
            //read
            var files = Directory.GetFiles(folderPath);
            var documentExtractor = new DocumentExtractor();
            var textProcessor = new TextProcessor();
            var multiTextFileReader = new MultiTextFileReader();
            var documentList = await documentExtractor
            .ExtractDocuments(files
            .Select(f=>Path.GetFullPath(f)).ToList(), 
            textProcessor,
            multiTextFileReader);

            // map data
            InvertedIndexMapper invertedIndexMapper = new InvertedIndexMapper();
            var InvertedIndex = invertedIndexMapper.Map(documentList);

            // search 
            var searchHandler = new SearchHandler(InvertedIndex);
            string input;
            while((input = Console.ReadLine()) != "end")
            {
                searchHandler.Search(input).result
                .documents.ForEach(result => 
                    Console.WriteLine(result));
            }
            
        }
        catch(DirectoryNotFoundException e)
        {
            Console.WriteLine(e.Message);
        }
    }
}