namespace FullTextSearch.Core;

public class Query
{
    public string Text { get; set; }
    public Dictionary<char, IEnumerable<string>> WordsBySign = new Dictionary<char, IEnumerable<string>>();

    public override bool Equals(object obj) 
    {
        if (ReferenceEquals(null, obj)) return false;
        if (obj.GetType() != GetType()) return false;
        return Equals((Query) obj);
    }

    public bool Equals(Query other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        if (!this.Text.Equals(other.Text)) return false;
        if(!this.WordsBySign.Count.Equals(other.WordsBySign.Count)) return false;
        foreach (var entry in this.WordsBySign)
        {
            if (!entry.Value.SequenceEqual(other.WordsBySign[entry.Key]))
            {
                return false;
            }
        }
        return true;
    }
}