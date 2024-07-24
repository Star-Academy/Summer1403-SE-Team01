public class Search
{
    public Search(Query query, Result result)
    {
        this.query = query;
        this.result = result;
    }
    public Query query { get; set; }
    public Result result { get; set; }
}