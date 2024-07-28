
public class PlusSearcher : ISearcher
{
    public char Sign {get; init;} = '+';
    public UniversalSearch universalSearch = new UniversalSearch();
    public List<Document> Search(Query query, Dictionary<string,List<Document>> dictionary)
    {
        var plusDocs = new List<Document>();
        if(query.signToWordDictionary[Sign].Count == 0)
            plusDocs = universalSearch.GetUniversal(dictionary);
        else
            query.signToWordDictionary[Sign].Where(s=>dictionary.ContainsKey(s)).ToList()
            .ForEach(x=> plusDocs = plusDocs
            .Union(dictionary[x]).ToList());
        return plusDocs;
    }
}