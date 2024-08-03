using FullTextSearch.Core;

namespace FullTextSearch.Controller.ResultController.Abstraction;

public interface INoSignedFilter
{
    IEnumerable<Document> Filter(IEnumerable<Document> documents,
        Dictionary<char, IEnumerable<Document>> documentsBySign);

}