namespace FullTextSearch.Core;

public class Document
{
    public string Name { get; set; }
    public string Path { get; set; }
    public string Text { get; set; }
    public IEnumerable<string> Words { get; set; }

    public override string ToString()
    {
        return $"Name:{Name}";
    }
    
    public override bool Equals(object obj) 
    {
        if (ReferenceEquals(null, obj)) return false;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Document) obj);
    }

    public bool Equals(Document other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return this.Name.Equals(other.Name) && this.Path.Equals(other.Path) && this.Text.Equals(other.Text) && this.Words.SequenceEqual(other.Words);
    }
}