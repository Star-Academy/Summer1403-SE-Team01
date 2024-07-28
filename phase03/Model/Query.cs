public class Query
{
    public string text { get; set; }
    public Dictionary<char, List<string>> signToWordDictionary {get; set;} = new Dictionary<char, List<string>>();

    public Query(string text)
    {
        this.text = text;
    }
}