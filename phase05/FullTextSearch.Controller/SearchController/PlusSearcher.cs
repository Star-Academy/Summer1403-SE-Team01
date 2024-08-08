using FullTextSearch.Core;

namespace FullTextSearch.Controller.SearchController;

public class PlusSearcher : ISearcher
{
    public char Sign {get; init;} = '+';
    public UniversalSearch universalSearch = new UniversalSearch(); // static this
    public IEnumerable<Document> Search(Query query, Dictionary<string,IEnumerable<Document>> dictionary)
    {
        IEnumerable<Document> plusDocs = new List<Document>();
        if(query.WordsBySign[Sign].ToList().Count == 0)
            plusDocs = universalSearch.GetUniversal(dictionary);
        else
            query.WordsBySign[Sign].Where(s=>dictionary.ContainsKey(s)).ToList()
                .ForEach(x=> plusDocs = plusDocs
                    .Union(dictionary[x]).ToList());
        return plusDocs;
    }
}