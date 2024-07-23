public class DocumentHandler
{
    public FileReader fileReader{get; set;}
    public FileEditor fileEditior{get; set;}

    public DocumentHandler(FileReader fileReader, FileEditor fileEditior)
    {
        this.fileReader = fileReader;
        this.fileEditior = fileEditior;
    }

    public async Task<Document> ExtractDoc(string filePath)
    {
        var str = await fileReader.ReadAsync(filePath);
        var myString = fileEditior.RemoveExtraSpace(str);
        var words = fileEditior.Split(fileEditior.ToUpper(myString));
        var doc = new Document(Path.GetFileName(filePath), filePath, myString, words);
        return doc;
    } 
    
}