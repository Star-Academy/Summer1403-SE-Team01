public interface IDocumentExtractor
{
    public Task<List<Document>> ExtractDocuments(List<string> filePaths, TextProcessor fileProcessor);
}