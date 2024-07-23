public class Mapper
{
    public List<Document> docList;
    public Mapper(List<Document> docList)
    {
        this.docList = docList;
    }
    public Dictionary<string, List<Document>> Map()
    {
        var tempDic = new Dictionary<string, HashSet<Document>>();
        foreach(var d in docList)
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
        var dic = tempDic.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToList());

        return dic;
    }
}
