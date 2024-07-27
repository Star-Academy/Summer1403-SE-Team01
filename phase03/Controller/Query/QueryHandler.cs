using System.Text.RegularExpressions;
//fill query.*
public class QueryHandler {

    public void Prepare(Query query, QueryProcessor queryProcessor, List<char> signs)
    {
        query.query = queryProcessor.ToUpper(query.query);
        var splittedText = queryProcessor.Split(query.query);

        foreach(var k in signs){
            var temp = Seperate(splittedText, k);
            splittedText = splittedText.Except(temp).ToList();
            query.map.Add(k, SepratePrefix(temp, k));
        }
        query.map[' '] = splittedText;
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
        input.ForEach(s=>result.Add(s.Substring(1, s.Length - 1)));
        
        return result;
    }
}