using System.Text.RegularExpressions;
public class FileEditor {
    //public Document document { get; set; }
    //clear useless
    //split
    //upercase
    //set->list
    public string ToUpper(string text)
    {
        var uppered = text.ToUpper();
        return uppered;
    }
    
    public List<string> Split(string text)
    {
        //Console.WriteLine("------------");
        var splittedText = text.Split(" ").ToList();
        return splittedText;
    }
    public string RemoveExtraSpace(string str)
    {
        var myString = Regex.Replace(str, @"\s+", " ");
        return myString;
    }

    public List<string> DeleteStopWord(List<string> word)
    {
        return null;
    }

}