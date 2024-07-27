public class DocumentHandler
{
    public async Task<Document> ExtractDoc(string filePath, FileReader fileReader, FileProcessor fileProcessor)
    {
        var str = await fileReader.ReadAsync(filePath);
        var myString = fileProcessor.RemoveExtraSpace(str);
        var words = fileProcessor.Split(fileProcessor.ToUpper(myString));
        var doc = new Document(Path.GetFileName(filePath), filePath, myString, words);
        return doc;
    } 
    
}