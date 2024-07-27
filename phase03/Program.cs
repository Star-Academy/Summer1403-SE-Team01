using System;
class Program
{
    static async Task Main(string[] args)
    {
        var fileReader = new FileReader();
        var fileEditor = new FileProcessor();
        var documentHandler = new DocumentHandler();
        var documentList = new List<Document>();
        //Collecting and operate data -> ./Resources/EnglishData
        string folderPath = Console.ReadLine();

        try
        {
            var files = Directory.GetFiles(folderPath);
            foreach (string file in files)
            {
                var doc = await documentHandler.ExtractDoc(folderPath +"/"+Path.GetFileName(file), fileReader, fileEditor);
                documentList.Add(doc);
            }
        }
        catch (DirectoryNotFoundException dirEx)
        {
            Console.WriteLine("Directory not found: " + dirEx.Message);
        }

        //Mapping data beetwen words and doc
        var mapper = new Mapper();
        var map = mapper.Map(documentList);

        //search
        var sh = new SearchHandler(map);
        string input;
        while((input = Console.ReadLine()) != "end")
        {
            var searchResult = sh.Search(input).result;
            foreach (var answer in searchResult.documents)
                Console.WriteLine(answer.name);
        }
    }
}
//cat +reza -Demand!
//cat reza