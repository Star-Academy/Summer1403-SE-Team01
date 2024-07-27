public class DocumentHandler
{
    public async Task<Document> ExtractDoc(string filePath, FileReader fileReader, FileEditor fileEditior)
    {
        var str = await fileReader.ReadAsync(filePath);
        var myString = fileEditior.RemoveExtraSpace(str);
        var words = fileEditior.Split(fileEditior.ToUpper(myString));
        var doc = new Document(Path.GetFileName(filePath), filePath, myString, words);
        return doc;
    } 
    
}