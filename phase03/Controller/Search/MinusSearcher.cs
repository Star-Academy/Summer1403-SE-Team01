
public class MinusSearcher : ISearcher
{
    public char sign {get; set;}

    public MinusSearcher(char sign) {
        this.sign = sign;
    }

    public List<Document> Search(Query query, Dictionary<string, List<Document>> dictionary)
    {
        var MinusDocs = new List<Document>();

        query.map[sign]
        .Where(s=>dictionary.ContainsKey(s))
        .ToList()
        .ForEach(x=>MinusDocs = MinusDocs
        .Union(dictionary[x])
        .ToList());
        
        return MinusDocs;
    }
}