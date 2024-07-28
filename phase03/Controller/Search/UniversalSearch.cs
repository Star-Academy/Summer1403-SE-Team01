
public class UniversalSearch : IUniversalSearch
{
    public List<Document> GetUniversal(Dictionary<string, List<Document>> dictionary)
    {
        var hashSet = new HashSet<Document>();
        dictionary.Values.ToList().ForEach(d=>d.ForEach(x=>hashSet.Add(x)));
        return hashSet.ToList();
    }
}