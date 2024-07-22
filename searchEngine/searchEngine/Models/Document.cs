namespace WebApplication2.Models;

public class Document
{
    public string name { get; set; }
    public string path { get; set; }
    public string text { get; set; }
    public List<string> words { get; set; }

    public Document(string name, string path, string text)
    {
        this.name = name;
        this.path = path;
        this.text = text;
    }
}