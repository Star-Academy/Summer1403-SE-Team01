
public class MinusSearcher : ISearcher
{
    public char Sign {get; init;} = '-';

    public List<Document> Search(Query query, Dictionary<string, List<Document>> dictionary)
    {
        var MinusDocs = new List<Document>();

        query.signToWordDictionary[Sign]
        .Where(s=>dictionary.ContainsKey(s)).ToList()
        .ForEach(x=>MinusDocs = MinusDocs
        .Union(dictionary[x]).ToList());;
        
        return MinusDocs;
    }
}