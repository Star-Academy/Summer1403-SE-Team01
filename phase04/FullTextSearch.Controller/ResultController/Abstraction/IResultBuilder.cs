using FullTextSearch.Core;

namespace FullTextSearch.Controller.ResultController;

public interface IResultBuilder
{
    public void BuildDocuments();
    public void BuildDocumentsBySign();
    public Result GetResult();
}