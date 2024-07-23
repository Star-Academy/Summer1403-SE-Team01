using System;
using System.Data;
using System.Data.Common;
using System.Dynamic;
using System.IO;

class Program
{
    static async Task Main(string[] args)
    {
        //read data 
        var fr = new FileReader();
        var fe = new FileEditor();

        //Collecting and operate data -> ./EnglishData
        var dh = new DocumentHandler(fr, fe);
        var docList = new List<Document>();
        string folderPath = Console.ReadLine(); 
        try
        {
            var files = Directory.GetFiles(folderPath);
            foreach (string file in files)
            {
                var doc = await dh.ExtractDoc( folderPath +"/"+Path.GetFileName(file));
                docList.Add(doc);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Folder(File) not found: ",e);
        }

        //Mapping data beetwen words and doc
        var mapper = new Mapper(docList);
        var map = mapper.Map();

        //search
        var sh = new SearchHandler(map);
        string input;
        while((input = Console.ReadLine()) != "end")
        {
            var searchResult = sh.search(input).result;
            foreach (var answer in searchResult.documents)
                Console.WriteLine(answer.name);
        }
    }
}
