
public class MinusSearcher : ISearcher
{
    public char Sign {get; init;} = '-';

    public List<Document> Search(Query query, Dictionary<string, List<Document>> dictionary)
    {
        var documents = query.signToWordDictionary[Sign]
            .Where(s => dictionary.ContainsKey(s))
            .SelectMany(s => dictionary[s])
            .Distinct()
            .ToList();
        
        return documents;
    }
}