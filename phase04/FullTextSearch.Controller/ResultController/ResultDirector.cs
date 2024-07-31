namespace FullTextSearch.Controller.ResultController;

public class ResultDirector : IResultDirector
{
    public void Construct(IResultBuilder resultBuilder)
    {
        resultBuilder.BuildDocumentsBySign();
        resultBuilder.BuildDocuments();
    }
}