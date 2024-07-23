using System.Linq.Expressions;

public class SearchHandler 
{
    public Dictionary<string, List<Document>> dic{ get; set;}
    public QueryHandler queryHandler;
    public ResultHandler resultHandler;
    public SearchHandler(Dictionary<string, List<Document>> dic)
    {
        this.dic = dic;
        queryHandler = new QueryHandler();
        resultHandler = new ResultHandler();
    }

    public Search search(string input)
    {
        var query = new Query(input);
        queryHandler.Prepare(query);

        var result = new Result();
        result.plusDocuments = ExtractPlusResult(query);
        result.MinesDocuments = ExtractMinusResult(query);
        result.OrdinaryDocuments = ExtractOrdinaryResult(query);


        if(query.plusQuery.Count() == 0)
            resultHandler.Prepare(result, false);
        else
            resultHandler.Prepare(result, true);

        var search = new Search(query, result);    
        return search;
    }

    public List<Document> ExtractPlusResult(Query query) {
        var plusDocs = new List<Document>();
        
        foreach(var s in query.plusQuery) {
            if (dic.ContainsKey(s))
                plusDocs = plusDocs.Union(dic[s]).ToList();
        }
        return plusDocs;
    }

    public List<Document> ExtractMinusResult(Query query) {
        var MinusDocs = new List<Document>();
        foreach(var s in query.minusQuery) {
            if (dic.ContainsKey(s))
                MinusDocs = MinusDocs.Union(dic[s]).ToList();
        }
        return MinusDocs;
    }

    public List<Document> ExtractOrdinaryResult(Query query) 
    {
        var ordinaryDocs = new List<Document>();

        if(query.ordinaryQuery.Count != 0 && dic.ContainsKey(query.ordinaryQuery[0])){
            ordinaryDocs = new List<Document>(dic[query.ordinaryQuery[0]]);
            foreach(var s in query.ordinaryQuery) {
                if (dic.ContainsKey(s))
                    ordinaryDocs = ordinaryDocs.Intersect(dic[s]).ToList();

                else
                {
                    ordinaryDocs.Clear();
                    break;
                }
            } return ordinaryDocs;
        }

        else if(query.ordinaryQuery.Count == 0)
            return getUniversal();
        
        else
            return ordinaryDocs;        
    }

    public List<Document> getUniversal() {
        HashSet<Document> hashSet = new HashSet<Document>();
            foreach(var d in dic)
            {
                foreach(var element in d.Value)
                {
                    hashSet.Add(element);
                }
                
            }
            return hashSet.ToList();
    }
}