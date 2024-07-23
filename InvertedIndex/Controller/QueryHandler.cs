using System.Text.RegularExpressions;
public class QueryHandler {

    public void Prepare(Query query)
    {
        query.query = UpperText(query);
        var splittedText = SplitText(query);
        query.plusQuery = SeperatePlus(splittedText);
        query.minusQuery = SeperateMinus(splittedText);
        query.ordinaryQuery = SeperateOrdinary(splittedText);
    }
    private List<string> SeperatePlus(List<string> listStr)
    {
        var result = new List<string>();
        foreach(var lstr in listStr)
        {
            if(lstr[0] == '+')
            {
                result.Add(lstr.Substring(1, lstr.Length - 1));
            }
        }
        return result;
    }
    private List<string> SeperateMinus(List<string> listStr)
    {
        var result = new List<string>();
        foreach(var lstr in listStr)
        {
            if(lstr[0] == '-')
            {
                result.Add(lstr.Substring(1, lstr.Length - 1));
            }
        }
        return result;
    }
    private List<string> SeperateOrdinary(List<string> listStr)
    {
        var result = new List<string>();
        foreach(var lstr in listStr)
        {
            if(lstr[0] != '-' && lstr[0] != '+')
            {
                result.Add(lstr);
            }
        }
        return result;
    }
    private string UpperText(Query query)
    {
        var uppered = query.query.ToUpper();
        return uppered;
    }
    private List<string> SplitText(Query query)
    {
        var myString = Regex.Replace(query.query, @"\s+", " ");
        var splittedText = myString.Split(" ").ToList();
        return splittedText;
    }

}