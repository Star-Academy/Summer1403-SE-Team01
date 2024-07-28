public interface IDocumentExtractor
{
    public Task<List<Document>> ExtractDocuments(List<string> filePaths, IProcessor textProcessor, IMultiReader multiTextFileReader);
}