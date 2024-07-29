//fill query.*
public class QueryExtractor : IQueryExtractor
{
    private readonly IQueryFormatter _queryProcessor;

    public QueryExtractor(IQueryFormatter queryProcessor)
    {
       _queryProcessor = queryProcessor;
    }

    public void Extract(Query query, List<char> signs)
    {
        query.text = _queryProcessor.ToUpper(query.text);
        var splittedText = _queryProcessor.Split(query.text);

        foreach(var sign in signs)
        {
            var texts = _queryProcessor.ExtractBySign(splittedText, sign);
            splittedText = splittedText.Except(texts).ToList();
            query.signToWordDictionary.Add(sign, _queryProcessor.SeparatePrefix(texts));
        }
        query.signToWordDictionary[' '] = splittedText;
    }


}