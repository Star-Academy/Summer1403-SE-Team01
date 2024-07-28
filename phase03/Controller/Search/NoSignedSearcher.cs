
public class NoSignedSearcher : ISearcher
{
    public char Sign {get; init;} = ' ';
    public UniversalSearch universalSearch = new UniversalSearch();
    public List<Document> Search(Query query, Dictionary<string, List<Document>> InvertedIndex)
    {
        var ordinaryDocs = new List<Document>();
        
        if(query.signToWordDictionary[Sign].Count == 0)
            ordinaryDocs = universalSearch.GetUniversal(InvertedIndex);
        
        else if(InvertedIndex.ContainsKey(query.signToWordDictionary[Sign][0]))
            ordinaryDocs = SearchIfContain(query, InvertedIndex);

        return ordinaryDocs;
    }
    private List<Document> SearchIfContain(Query query, Dictionary<string, List<Document>> InvertedIndex)
    {
        var ordinaryDocs = new List<Document>();
        InvertedIndex[query.signToWordDictionary[Sign][0]]
        .ForEach(s=>ordinaryDocs.Add(s));
        foreach(var document in query.signToWordDictionary[Sign])
        {
            if (InvertedIndex.ContainsKey(document))
                ordinaryDocs = ordinaryDocs.Intersect(InvertedIndex[document]).ToList();
            else
            {
                ordinaryDocs.Clear();
                break;
            }
        }
        return ordinaryDocs;
    }
}