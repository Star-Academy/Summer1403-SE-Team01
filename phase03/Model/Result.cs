public class Result
{
    public List<Document> documents { get; set; }    
    public Dictionary<char, List<Document>> signToDocumentListDictionary {get; set;} = new Dictionary<char, List<Document>>();
}