public class InvertedIndexMapper : IMapper
{
    public Dictionary<string, List<Document>> Map(List<Document> documentList)
    {
        var invertedIndex = documentList
        .SelectMany(d => d.Words.Select(w => new { Word = w, Document = d }))
        .GroupBy(x => x.Word)
        .ToDictionary(g => g.Key, g => g.Select(x => x.Document).ToList());

        return invertedIndex;
    }
}
