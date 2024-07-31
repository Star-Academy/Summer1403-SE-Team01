using FullTextSearch.Core;

namespace FullTextSearch.Controller.SearchController;

public class MinusSearcher : ISearcher
{
    public char Sign {get; init;} = '-';

    public IEnumerable<Document> Search(Query query, Dictionary<string, IEnumerable<Document>> dictionary)
    {
        var documents = query.WordsBySign[Sign]
            .Where(s => dictionary.ContainsKey(s))
            .SelectMany(s => dictionary[s])
            .Distinct()
            .ToList();
        
        return documents;
    }
}