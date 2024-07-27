
public class NoSignedSearcher : ISearcher
{
    public char sign {get; set;}

    public NoSignedSearcher(char sign) {
        this.sign = sign;
    }
    public List<Document> Search(Query query, Dictionary<string, List<Document>> dictionary)
    {
        var ordinaryDocs = new List<Document>();

        if(query.map[sign].Count == 0)
            ordinaryDocs = GetUniversal(dictionary);
        
        else if(dictionary.ContainsKey(query.map[sign][0]))
        {
            dictionary[query.map[sign][0]]
            .ForEach(s=>ordinaryDocs.Add(s));

            foreach(var s in query.map[sign])
            {
                //ordinaryDocs = dictionary.ContainsKey(s) ?  ordinaryDocs.Intersect(dictionary[s]).ToList() : new List<Document>();    

                if (dictionary.ContainsKey(s))
                    ordinaryDocs = ordinaryDocs.Intersect(dictionary[s]).ToList();

                else
                {
                    ordinaryDocs.Clear();
                    break;
                }
                
            }
        }
        return ordinaryDocs;
    }
    private List<Document> GetUniversal(Dictionary<string,List<Document>> dictionary) {
        HashSet<Document> hashSet = new HashSet<Document>();
            dictionary.Values.ToList().ForEach(d=>d.ForEach(x=>hashSet.Add(x)));
            return hashSet.ToList();
    }
}