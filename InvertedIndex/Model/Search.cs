public class Search
{
    public Search(Query query)
    {
        this.query = query;
    }
    public Query query { get; set; }
    public Result result { get; set; }
}