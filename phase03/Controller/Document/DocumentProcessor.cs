using System.Text.RegularExpressions;
public class TextProcessor : IProcessor{

    private static TextProcessor instance;

    private TextProcessor() {}

    public static TextProcessor getInstance() {
        if(instance == null) {
            instance = new TextProcessor();
        }
        return instance;
    }

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
}