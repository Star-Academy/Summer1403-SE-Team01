public class DocumentHandler
{
    public FileReader fr{get; set;}
    public FileEditor fe{get; set;}

    public DocumentHandler(FileReader fr, FileEditor fe)
    {
        this.fr = fr;
        this.fe = fe;
    }

    public async Task<Document> ExtractDoc(string dockPath)
    {
        var str = await fr.ReadAsync("./EnglishData/"+Path.GetFileName(dockPath));
        var myString = fe.RemoveExtraSpace(str);
        var words = fe.Split(fe.ToUpper(myString));
        var doc = new Document(Path.GetFileName(dockPath), "./EnglishData/"+Path.GetFileName(dockPath), myString, words);
        return doc;
    } 
    
}