public class Query
{
    public Query(string input)
    {
        this.query = input;
        this.map = new Dictionary<char, List<string>>();
    }
    public string query { get; set; }
  
    public Dictionary<char, List<string>> map {get; set;}
}