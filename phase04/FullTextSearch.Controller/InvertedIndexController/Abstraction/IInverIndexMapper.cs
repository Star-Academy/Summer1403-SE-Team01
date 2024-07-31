using FullTextSearch.Core;

namespace FullTextSearch.Controller.InvertedIndexController;

public interface IInvertedIndexMapper {
        public Dictionary<string, IEnumerable<Document>> Map(IEnumerable<Document> documents);
}