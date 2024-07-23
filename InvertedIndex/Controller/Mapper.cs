public class Mapper
{
    public List<Document> documentList;
    public Mapper(List<Document> documentList)
    {
        this.documentList = documentList;
    }
    public Dictionary<string, List<Document>> Map()
    {
        var tempDic = new Dictionary<string, HashSet<Document>>();
        foreach(var d in documentList)
        {
            foreach(var w in d.words)
            {
                if (!tempDic.ContainsKey(w))
                {
                    tempDic[w] = new HashSet<Document>();
                }
                tempDic[w].Add(d);
            }
        }
        var dictionary = tempDic.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToList());

        return dictionary;
    }
}
