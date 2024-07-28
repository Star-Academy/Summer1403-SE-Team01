public interface ISearcher
{
    public char Sign {get; init;}
    public List<Document> Search(Query query, Dictionary<string,List<Document>> dictionary);
}