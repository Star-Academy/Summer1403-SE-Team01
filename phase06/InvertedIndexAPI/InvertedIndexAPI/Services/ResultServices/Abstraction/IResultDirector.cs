using FullTextSearch.Core;

namespace FullTextSearch.Controller.ResultController.Abstraction;

public interface IResultDirector
{
    void Construct(IResultBuilder resultBuilder, Query query,
        Dictionary<string, IEnumerable<Document>> invertedIndex);
}