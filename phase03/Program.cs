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
            DocumentExtractor documentExtractor = new DocumentExtractor();
            TextProcessor textProcessor = new TextProcessor();
            var documentList = await documentExtractor
            .ExtractDocuments(files
            .Select(f=>Path.GetFullPath(f)).ToList(), 
            textProcessor);

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
            Console.WriteLine("Path of folder doesn't  exists: ", e.Message);
        }
    }
}