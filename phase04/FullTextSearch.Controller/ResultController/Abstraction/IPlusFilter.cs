using FullTextSearch.Core;

namespace FullTextSearch.Controller.ResultController.Abstraction;

public interface IPlusFilter
{
    IEnumerable<Document> Filter(IEnumerable<Document> documents,
        Dictionary<char, IEnumerable<Document>> documentsBySign);

}