public interface ISignable
{
    public char sign {get; set;}
    public List<Document> Extract(Query query, Dictionary<string,List<Document>> dictionary);
}