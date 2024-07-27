using System.Linq.Expressions;

public class SearchHandler 
{
    public Dictionary<string, List<Document>> dictionary{ get; set;}//inverted index
    public QueryHandler queryHandler;
    public ResultHandler resultHandler;

    public SearchHandler(Dictionary<string, List<Document>> dictionary)
    {
        this.dictionary = dictionary;
        queryHandler = new QueryHandler();
        resultHandler = new ResultHandler();
    }

    public Search Search(string input)
    {
        var query = new Query(input);
        queryHandler.Prepare(query,new QueryEditor());

        var result = new Result();
        
        result.plusDocuments = ExtractPlusResult(query);
        result.MinesDocuments = ExtractMinusResult(query);
        result.OrdinaryDocuments = ExtractOrdinaryResult(query);

        resultHandler.Prepare(result);

        var search = new Search(query, result);    
        return search;
    }

    private List<Document> ExtractPlusResult(Query query) {
        var plusDocs = new List<Document>();
        if(query.plusQuery.Count == 0)
            return GetUniversal();
        else
        {
            foreach(var s in query.plusQuery) {
                if (dictionary.ContainsKey(s))
                    plusDocs = plusDocs.Union(dictionary[s]).ToList();
            }
            return plusDocs;
        }
    }

    private List<Document> ExtractMinusResult(Query query) {
        var MinusDocs = new List<Document>();
        foreach(var s in query.minusQuery) {
            if (dictionary.ContainsKey(s))
                MinusDocs = MinusDocs.Union(dictionary[s]).ToList();
        }
        return MinusDocs;
    }

    private List<Document> ExtractOrdinaryResult(Query query) 
    {
        var ordinaryDocs = new List<Document>();
        if(query.ordinaryQuery.Count == 0)
            ordinaryDocs = GetUniversal();
        else if(dictionary.ContainsKey(query.ordinaryQuery[0]))
        {
            dictionary[query.ordinaryQuery[0]].ForEach(s=>ordinaryDocs.Add(s));

            foreach(var s in query.ordinaryQuery)
            {
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
        
    private List<Document> GetUniversal() {
        HashSet<Document> hashSet = new HashSet<Document>();
            foreach(var d in dictionary)
            {
                foreach(var element in d.Value)
                {
                    hashSet.Add(element);
                }
                
            }
            return hashSet.ToList();
    }
}