public class Query
{
    public Query(string input)
    {
        this.query = input;
    }
    public string query { get; set; }
    public List<string> plusQuery {get; set;}
    public List<string> minusQuery {get; set;}
    public List<string> ordinaryQuery {get;set;}
}