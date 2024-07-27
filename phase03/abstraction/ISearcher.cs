public interface ISearcher
{
    public char sign {get; set;}
    public List<Document> Search(Query query, Dictionary<string,List<Document>> dictionary);
}