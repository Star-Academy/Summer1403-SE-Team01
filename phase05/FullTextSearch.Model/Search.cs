namespace FullTextSearch.Core;

public class Search
{
    private Query Query { get; set; }
    private Result Result { get; set; }

    public Search(Query query, Result result)
    {
        Query = query;
        Result = result;
    }
}