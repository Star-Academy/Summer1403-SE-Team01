using FullTextSearch.Core;

namespace FullTextSearch.Controller.ResultController;

public class NoSignedFilterer : IFilterer
{
    public void Filter(Result result)
    {
        result.documents = result.documentsBySign[' '];
    }
}