using System.Collections;
using FullTextSearch.Core;

namespace FullTextSearch.Controller.InvertedIndexController;

public interface IInvertedIndexMapper {
        Dictionary<string, IEnumerable<Document>> Map(IEnumerable<Document> documents);
}