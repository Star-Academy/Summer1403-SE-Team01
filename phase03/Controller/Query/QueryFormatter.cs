
using System.Text.RegularExpressions;

public class QueryFormatter : IQueryFormatter
{
    public string ToUpper(string text)
    {
        var uppered = text.ToUpper();
        return uppered;
    }

    public List<string> Split(string text)
    {
        var splittedText = text.Split(" ").ToList();
        return splittedText;
    }

    public string RemoveExtraSpace(string str)
    {
        var myString = Regex.Replace(str, @"\s+", " ");
        return myString;
    }

    public List<string> ExtractBySign(List<string> list, char c)
    {
        return list.Where(x => x[0]==c).ToList();
    }

    public List<string> SeparatePrefix(List<string> input)
    {
        var result = input.Select(s=>s.Substring(1, s.Length - 1)).ToList();
        
        return result;
    }
}