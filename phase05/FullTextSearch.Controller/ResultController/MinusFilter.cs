using FullTextSearch.Controller.ResultController.Abstraction;
using FullTextSearch.Core;

namespace FullTextSearch.Controller.ResultController;

public class MinusFilter : IFilter
{
    public IEnumerable<Document> Filter(IEnumerable<Document> documents, Dictionary<char, IEnumerable<Document>> documentsBySign)
    {
        return documents.Except(documentsBySign['-']);
    }
}