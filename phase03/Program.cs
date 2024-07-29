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
            var textProcessor = new DocumentFormatter();
            var multiTextFileReader = new MultiTextFileReader();
            var documentExtractor = new DocumentExtractor(textProcessor, multiTextFileReader);

            var documentList = await documentExtractor
            .Extract(files
            .Select(f=>Path.GetFullPath(f)).ToList());

            // map data
            InvertedIndexMapper invertedIndexMapper = new InvertedIndexMapper();
            var InvertedIndex = invertedIndexMapper.Map(documentList);

            // search 
            var searchHandler = new SearchHandler(InvertedIndex, new SearcherDriver(), new FiltererDirver(), new QueryExtractor(new QueryFormatter()));
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