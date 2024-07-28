public class InvertedIndexMapper : IMapper
{
    private static InvertedIndexMapper instance;

    private InvertedIndexMapper() {}

    public static InvertedIndexMapper getInstance() {
        if(instance == null) {
            instance = new InvertedIndexMapper();
        }
        return instance;
    }

    public Dictionary<string, List<Document>> Map(List<Document> documentList)
    {
        var invertedIndex = documentList
        .SelectMany(d => d.Words.Select(w => new { Word = w, Document = d }))
        .GroupBy(x => x.Word)
        .ToDictionary(g => g.Key, g => g.Select(x => x.Document).ToList());

        return invertedIndex;
    }
}
