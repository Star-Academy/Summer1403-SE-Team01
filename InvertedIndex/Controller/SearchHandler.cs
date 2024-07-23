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

    private List<Document> ExtractPlusResult(Query query) {
        var plusDocs = new List<Document>();
        
        foreach(var s in query.plusQuery) {
            if (dictionary.ContainsKey(s))
                plusDocs = plusDocs.Union(dictionary[s]).ToList();
        }
        return plusDocs;
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

        if(query.ordinaryQuery.Count != 0 && dictionary.ContainsKey(query.ordinaryQuery[0]))
            return ExtractFindOrdinaryResult(ordinaryDocs, query);

        else if(query.ordinaryQuery.Count == 0)
            return GetUniversal();
        
        else
            return ordinaryDocs;        
    }

    private List<Document> ExtractFindOrdinaryResult(List<Document> ordinaryDocs, Query query) 
    {
        ordinaryDocs = new List<Document>(dictionary[query.ordinaryQuery[0]]);
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