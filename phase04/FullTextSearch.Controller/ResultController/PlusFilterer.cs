using FullTextSearch.Core;

namespace FullTextSearch.Controller.ResultController;

public class PlusFilterer : IFilterer
{
    public void Filter(Result result)
    {
        result.documents = result.documents.Intersect(result.documentsBySign['+']).ToList();
    }
}