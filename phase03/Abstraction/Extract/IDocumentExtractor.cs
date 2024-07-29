public interface IDocumentExtractor
{
    public Task<List<Document>> Extract(List<string> filePaths);
}