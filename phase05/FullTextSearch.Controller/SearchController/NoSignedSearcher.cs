using FullTextSearch.Controller.SearchController.Abstraction;
using FullTextSearch.Core;

namespace FullTextSearch.Controller.SearchController;

public class NoSignedSearcher : ISearcher
{
    public char Sign {get; init;} = ' ';
    private readonly UniversalSearch _universalSearch = new UniversalSearch();
    
    public IEnumerable<Document> Search(Query query, Dictionary<string, IEnumerable<Document>> invertedIndex)
    {
        var ordinaryDocs = _universalSearch.GetUniversal(invertedIndex);

        if (query.WordsBySign[Sign].ToList().Count != 0)
        {
            try
            {
                query.WordsBySign[Sign]
                    .ToList().ForEach(w => ordinaryDocs = ordinaryDocs.Intersect(invertedIndex[w]));
            } 
            catch(KeyNotFoundException e) 
            {
                ordinaryDocs.ToList().Clear();
            }
        }

        return ordinaryDocs;
    }
}