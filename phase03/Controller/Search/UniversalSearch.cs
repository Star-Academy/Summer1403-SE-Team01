
public class UniversalSearch : IUniversalSearch
{
    public List<Document> GetUniversal(Dictionary<string, List<Document>> invertedIndex)
    {
        var universalList = invertedIndex.Values.SelectMany(d => d).Distinct();
        return universalList.ToList();
    }
}