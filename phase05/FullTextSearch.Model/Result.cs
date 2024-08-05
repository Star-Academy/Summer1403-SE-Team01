namespace FullTextSearch.Core;

public class Result
{
    public Result()
    {
        documents = new List<Document>();
        documentsBySign = new Dictionary<char, IEnumerable<Document>>();
    }
    public IEnumerable<Document> documents { get; set; }    
    public Dictionary<char, IEnumerable<Document>> documentsBySign {get; set;}
    
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
        if (!this.documents.SequenceEqual(other.documents)) return false;
        if(!this.documentsBySign.Count.Equals(other.documentsBySign.Count)) return false;
        foreach (var entry in this.documentsBySign)
        {
            if (!entry.Value.SequenceEqual(other.documentsBySign[entry.Key]))
            {
                return false;
            }
        }

        return true;
    }
}