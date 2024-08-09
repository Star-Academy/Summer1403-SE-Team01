using FullTextSearch.Controller.SearchController.Abstraction;
using FullTextSearch.Core;

namespace FullTextSearch.Controller.SearchController;

public class PlusSearcher : ISearcher
{
    public char Sign {get; init;} = '+';
    private readonly UniversalSearch _universalSearch = new UniversalSearch(); 
    public IEnumerable<Document> Search(Query query, Dictionary<string,IEnumerable<Document>> dictionary)
    {
        IEnumerable<Document> plusDocs = new List<Document>();
        
        if(query.WordsBySign[Sign].ToList().Count == 0)
            plusDocs = _universalSearch.GetUniversal(dictionary);
        
        else
        {
            query.WordsBySign[Sign].Where(s=>dictionary.ContainsKey(s)).ToList()
                .ForEach(x=> plusDocs = plusDocs
                    .Union(dictionary[x]).ToList());
        }
        
        return plusDocs;
    }
}