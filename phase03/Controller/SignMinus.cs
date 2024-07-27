
public class SignMinus : ISignable
{
    public char sign {get; set;}
    public SignMinus(char sign) {
        this.sign = sign;
    }

    public List<Document> Extract(Query query, Dictionary<string, List<Document>> dictionary)
    {
        var MinusDocs = new List<Document>();
        query.map[sign].Where(s=>dictionary.ContainsKey(s)).ToList()
        .ForEach(x=>MinusDocs = MinusDocs
        .Union(dictionary[x]).ToList());
        return MinusDocs;
    }
}