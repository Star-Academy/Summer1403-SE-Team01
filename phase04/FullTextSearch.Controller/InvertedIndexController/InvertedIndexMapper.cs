using FullTextSearch.Core;

namespace FullTextSearch.Controller.InvertedIndexController;

public class InvertedIndexMapper : IInvertedIndexMapper
{
    public Dictionary<string, IEnumerable<Document>> Map(IEnumerable<Document> documents)
    {
        var invertedIndex = documents
            .SelectMany(document => document.Words.Select(word => new { Word = word, Document = document }))
            .GroupBy(x => x.Word)
            .ToDictionary(entry => entry.Key, entry => entry.Select(x => x.Document));

        return invertedIndex;
    }
}