public class InvertedIndexMapper : IInverIndexMapper
{
    public Dictionary<string, List<Document>> Map(List<Document> documentList)
    {
        var invertedIndex = documentList
        .SelectMany(document => document.Words.Select(word => new { Word = word, Document = document }))
        .GroupBy(x => x.Word)
        .ToDictionary(entry => entry.Key, entry => entry.Select(x => x.Document).ToList());

        return invertedIndex;
    }
}
