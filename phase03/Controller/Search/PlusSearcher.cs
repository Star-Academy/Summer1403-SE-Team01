
public class PlusSearcher : ISearcher
{
    public char Sign {get; init;} = '+';

    public List<Document> Search(Query query, Dictionary<string,List<Document>> dictionary)
    {
        var plusDocs = new List<Document>();
        if(query.signToWordDictionary[Sign].Count == 0)
            plusDocs = GetUniversal(dictionary);
        else
            query.signToWordDictionary[Sign].Where(s=>dictionary.ContainsKey(s)).ToList()
            .ForEach(x=> plusDocs = plusDocs
            .Union(dictionary[x]).ToList());
        return plusDocs;
    }
    private List<Document> GetUniversal(Dictionary<string,List<Document>> dictionary)
    {
        var hashSet = new HashSet<Document>();
            dictionary.Values.ToList().ForEach(d=>d.ForEach(x=>hashSet.Add(x)));
            return hashSet.ToList();
    }
}