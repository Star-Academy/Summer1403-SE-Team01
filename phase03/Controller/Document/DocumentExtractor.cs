public class DocumentExtractor : IDocumentExtractor
{
    IDocumentFormatter textProcessor {get; set;}
    IMultiReader multiTextFileReader  {get; set;}
    public DocumentExtractor(IDocumentFormatter textProcessor, IMultiReader multiTextFileReader)
    {
        this.textProcessor = textProcessor;
        this.multiTextFileReader = multiTextFileReader;
    }
    public async Task<List<Document>> Extract(List<string> filePaths)
    {
        var documents = new List<Document>();
        var texts = await multiTextFileReader.MultiReadAsync(filePaths);
        
        for(int i=0; i<filePaths.Count; i++)
        {
            var words = textProcessor.Split(textProcessor.ToUpper(textProcessor.RemoveExtraSpace(texts[i])));
            var document = new Document(Path.GetFileName(filePaths[i]), filePaths[i], texts[i], words);
            documents.Add(document);
        }
        
        return documents;
    }
}