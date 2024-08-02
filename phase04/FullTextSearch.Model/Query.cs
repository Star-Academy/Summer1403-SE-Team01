namespace FullTextSearch.Core
{
    public class Query
    {
        public string Text { get; set; }
        public Dictionary<char, IEnumerable<string>> WordsBySign { get; set; } = new Dictionary<char, IEnumerable<string>>();
    }
}