public class Result
{
    public Result()
    {
        map = new Dictionary<char, List<Document>>();
    }
    
    public List<Document> documents { get; set; }    
    public Dictionary<char, List<Document>> map {get; set;}

}