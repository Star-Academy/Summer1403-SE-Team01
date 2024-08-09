using FullTextSearch.Core;

namespace FullTextSearch.Controller.InvertedIndexController.Abstraction;

public interface IInvertedIndexMapper {
        Dictionary<string, IEnumerable<Document>> Map(IEnumerable<Document> documents);
}