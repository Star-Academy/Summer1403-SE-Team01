public class Mapper
{
    public List<Document> dockList;
    public Mapper(List<Document> dockList)
    {
        this.dockList = dockList;
    }
    public Dictionary<string, List<Document>> Map()
    {
        Dictionary<string, List<Document>> dic = new Dictionary<string, List<Document>>();
        var unique = ExtractTerms(this.dockList);
        
        foreach(var s in unique)
        {
            List<Document> l = new List<Document>(); 
            foreach(var d in this.dockList) {
                if(d.words.Contains(s)) {
                    l.Add(d);
                }
            }
            dic.Add(s, l);
        }
        return dic;
        
    }
    public HashSet<string> ExtractTerms(List<Document> docList)
    {
        HashSet<string> terms = new HashSet<string>();
        dockList.ForEach(d=>d.words.ForEach(w=>terms.Add(w)));
        // foreach(var d in docList)
        // {
        //     foreach(var s in d.words) 
        //     {
        //         terms.Add(s);
        //     }
        // }
        return terms;
    }
}
