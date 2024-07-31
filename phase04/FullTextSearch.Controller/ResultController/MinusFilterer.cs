namespace FullTextSearch.Controller.ResultController;

public class MinusFilterer : IFilterer
{
    public void Filter(FullTextSearch.Core.Result result)
    {
        result.documents = result.documents.Except(result.documentsBySign['-']).ToList();
    }
}