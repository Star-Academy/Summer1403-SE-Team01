using System.Text.RegularExpressions;
//fill query.*
public class QueryExtractor : IQueryExtractor
{
    private readonly IQueryProcessor _queryProcessor;

    public QueryExtractor(IQueryProcessor queryProcessor)
    {
       _queryProcessor = queryProcessor;
    }

    public void ExtractQuery(Query query, List<char> signs)
    {
        query.text = _queryProcessor.ToUpper(query.text);
        var splittedText = _queryProcessor.Split(query.text);

        foreach(var sign in signs) {
            var temp = _queryProcessor.ExtractBySign(splittedText, sign);
            splittedText = splittedText.Except(temp).ToList();
            query.signToWordDictionary.Add(sign, _queryProcessor.SeparatePrefix(temp));
        }
        query.signToWordDictionary[' '] = splittedText;
    }


}