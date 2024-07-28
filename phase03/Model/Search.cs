public class Search
{
    public Query query { get; set; }
    public Result result { get; set; }

    public Search(Query query, Result result)
    {
        this.query = query;
        this.result = result;
    }
}