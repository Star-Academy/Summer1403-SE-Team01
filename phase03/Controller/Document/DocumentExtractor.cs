public class DocumentExtractor : IDocumentExtractor
{
    public async Task<List<Document>> ExtractDocuments(List<string> filePaths, IProcessor textProcessor, IMultiReader multiTextFileReader)
    {
        var documents = new List<Document>();
        var texts = await multiTextFileReader.MultiReadAsync(filePaths);
        
        for(int i=0; i<filePaths.Count; i++)
        {
            var words = textProcessor.Split(textProcessor.ToUpper(textProcessor.RemoveExtraSpace(texts[i])));//To do
            var document = new Document(Path.GetFileName(filePaths[i]), filePaths[i], texts[i], words);
            documents.Add(document);
        }
        
        return documents;
    }
}