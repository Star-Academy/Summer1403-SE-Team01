using System;
using System.Data.Common;
using System.Dynamic;
using System.IO;

class Program
{
    static async Task Main(string[] args)
    {
        //read data 
        FileReader fr = new FileReader();
        FileEditor fe = new FileEditor();

        //Collecting and operate data  
        DocHandler dh = new DocHandler(fr, fe);
        List<Document> docList = new List<Document>();
        string folderPath = @"./EnglishData";
        string[] files = Directory.GetFiles(folderPath);

        foreach (string file in files)
        {
            var doc = await dh.ExtractDoc("./EnglishData/"+Path.GetFileName(file));
            docList.Add(doc);
            Console.WriteLine(Path.GetFileName(file));
        }

        //Mapping data beetwen words and doc
        Mapper mapper = new Mapper(docList);
        var map = mapper.Map();

        //search
        SearchHandler sh = new SearchHandler(map);
        string input;
        while((input = Console.ReadLine()) != "end")
        {
            var searchResult = sh.search(input).result;
            foreach (var answer in searchResult.documents)
                Console.WriteLine(answer.name);
        }
    }
}
