namespace FullTextSearch.Core;

public class Result
{
    public Result()
    {
        Documents = new List<Document>();
        DocumentsBySign = new Dictionary<char, IEnumerable<Document>>();
    }
    public IEnumerable<Document> Documents { get; set; }    
    public Dictionary<char, IEnumerable<Document>> DocumentsBySign {get; set;}
    
    public override bool Equals(object obj) 
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Result) obj);
    }

    public bool Equals(Result other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        if (!this.Documents.SequenceEqual(other.Documents)) return false;
        if(!this.DocumentsBySign.Count.Equals(other.DocumentsBySign.Count)) return false;
        foreach (var entry in this.DocumentsBySign)
        {
            if (!entry.Value.SequenceEqual(other.DocumentsBySign[entry.Key]))
            {
                return false;
            }
        }

        return true;
    }
}