using FullTextSearch.Controller.SearchController.Abstraction;
using FullTextSearch.Core;

namespace FullTextSearch.Controller.SearchController;

public class NoSignedSearcher : ISearcher
{
    public char Sign {get; init;} = ' ';
    private readonly UniversalSearch _universalSearch = new UniversalSearch();
    
    public IEnumerable<Document> Search(Query query, Dictionary<string, IEnumerable<Document>> invertedIndex)
    {
        IEnumerable<Document> ordinaryDocs = new List<Document>();

        if(query.WordsBySign[Sign].ToList().Count == 0)
            ordinaryDocs = _universalSearch.GetUniversal(invertedIndex);
        
        else
        {
            try
            {
                ordinaryDocs = query.WordsBySign[Sign]
                    .Select(w=>invertedIndex[w])
                    .SelectMany(d=>d)
                    .Distinct()
                    .ToList();
            } 
            catch(KeyNotFoundException e) 
            {
                ordinaryDocs.ToList().Clear();
            }
        }
        
        return ordinaryDocs;
    }
}