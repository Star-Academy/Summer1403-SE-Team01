
public class NoSignedSearcher : ISearcher
{
    public char Sign {get; init;} = ' ';
    public UniversalSearch universalSearch = new UniversalSearch();
    
    public List<Document> Search(Query query, Dictionary<string, List<Document>> InvertedIndex)
    {
        var ordinaryDocs = new List<Document>();

        if(query.signToWordDictionary[Sign].Count == 0)
            ordinaryDocs = universalSearch.GetUniversal(InvertedIndex);
        
        else
        {
            try 
            {
            ordinaryDocs = query.signToWordDictionary[Sign]
            .Select(w=>InvertedIndex[w])
            .SelectMany(d=>d)
            .Distinct()
            .ToList();
            } 
            catch(KeyNotFoundException e) 
            {
                ordinaryDocs.Clear();
            }
        }
        
        return ordinaryDocs;
    }
}