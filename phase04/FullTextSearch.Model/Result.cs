namespace FullTextSearch.Core;

public class Result
{
    public Result()
    {
        documentsBySign = new Dictionary<char, IEnumerable<Document>>();
    }
    public IEnumerable<Document> documents { get; set; }    
    public Dictionary<char, IEnumerable<Document>> documentsBySign {get; set;}
}