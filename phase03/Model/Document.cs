public class Document 
{
    public string Name { get; }
    public string Path { get; }
    public string Text { get; }
    public List<string> Words { get; }

    public Document(string name, string path, string text, List<string> words)
    {
        Name = name;
        Path = path;
        Text = text;
        Words = new List<string>(words); // Create a copy to ensure immutability
    }
    public override string ToString()
    {
        return $"Name:{Name}";
    }
}
