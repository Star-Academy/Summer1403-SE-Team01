
using FullTextSearch.Core;

namespace FullTextSearch.Controller.SearchController;

public class NoSignedSearcher : ISearcher
{
    public char Sign {get; init;} = ' ';
    public UniversalSearch universalSearch = new UniversalSearch();
    
    public IEnumerable<Document> Search(Query query, Dictionary<string, IEnumerable<Document>> InvertedIndex)
    {
        IEnumerable<Document> ordinaryDocs = new List<Document>();

        if(query.WordsBySign[Sign].ToList().Count == 0)
            ordinaryDocs = universalSearch.GetUniversal(InvertedIndex);
        
        else
        {
            try 
            {
                ordinaryDocs = query.WordsBySign[Sign]
                    .Select(w=>InvertedIndex[w])
                    .SelectMany(d=>d)
                    .Distinct()
                    .ToList();
            } 
            catch(KeyNotFoundException e) 
            {
                ordinaryDocs.ToList().Clear(); //
            }
        }
        
        return ordinaryDocs;
    }
}