using FullTextSearch.Core;

namespace FullTextSearch.Controller.SearchController;

public class MinusSearcher : ISearcher
{
    public char Sign {get; init;} = '-';

    public IEnumerable<Document> Search(Query query, Dictionary<string, IEnumerable<Document>> invertedIndex)
    {
        var documents = query.WordsBySign[Sign]
            .Where(s => invertedIndex.ContainsKey(s))
            .SelectMany(s => invertedIndex[s])
            .Distinct()
            .ToList();
        
        return documents;
    }
}