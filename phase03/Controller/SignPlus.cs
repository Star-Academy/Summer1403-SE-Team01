
public class SignPlus : ISignable//sign
{
    public char sign { get; set; }

    public SignPlus(char sign) {
        this.sign = sign;
    }

    public List<Document> Extract(Query query, Dictionary<string,List<Document>> dictionary)
    {
        var plusDocs = new List<Document>();
        if(query.map[sign].Count == 0)
            plusDocs = GetUniversal(dictionary);
        else
            query.map[sign].Where(s=>dictionary.ContainsKey(s)).ToList()
            .ForEach(x=> plusDocs = plusDocs
            .Union(dictionary[x]).ToList());
        return plusDocs;
    }
    private List<Document> GetUniversal(Dictionary<string,List<Document>> dictionary) {
        HashSet<Document> hashSet = new HashSet<Document>();
            dictionary.Values.ToList().ForEach(d=>d.ForEach(x=>hashSet.Add(x)));
            return hashSet.ToList();
    }
}