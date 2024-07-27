using System.Text.RegularExpressions;
//fill query.*
public class QueryHandler {

    public void Prepare(Query query, QueryEditor qe)
    {
        query.query = qe.ToUpper(query.query);
        var splittedText = qe.Split(query.query);
        
        query.plusQuery = Seperate(splittedText, '+');
        splittedText = splittedText.Except(query.plusQuery).ToList();
        query.minusQuery = Seperate(splittedText, '-');
        splittedText = splittedText.Except(query.minusQuery).ToList();
        query.ordinaryQuery = splittedText;


        query.plusQuery = SepratePrefix(query.plusQuery, '+');
        query.minusQuery = SepratePrefix(query.minusQuery, '-');

    }

    private List<string> Seperate(List<string> list, char c)
    {
        var result = new List<string>();        
        list.Where(x => x[0]==c).ToList().ForEach(l=>result.Add(l));

        return result;

    }
    private List<string> SepratePrefix(List<string> input, char c)
    {

        var result = new List<string>();
        foreach(var s in input) {
            result.Add(s.Substring(1, s.Length - 1));
        } 
        return result;
    }
}