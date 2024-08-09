using FullTextSearch.Controller.QueryController.Abstraction;

namespace FullTextSearch.Controller.QueryController;

public class QueryDirector : IQueryDirector
{
    public void Construct(string text, IQueryBuilder queryBuilder)
    {
        queryBuilder.BuildText(text);
        queryBuilder.BuildWordsBySign(new List<IWordCollector>()
        {
            new MinusWordCollector(), 
            new PlusWordsCollector(), 
            new NoSignedWordCollector()
            
        });
    }
}