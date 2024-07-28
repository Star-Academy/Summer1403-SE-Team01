public class DocumentExtractor : IDocumentExtractor
{
    public async Task<List<Document>> ExtractDocuments(List<string> filePaths, TextProcessor fileProcessor)
    {
        List<Document> documents = new List<Document>();
        MultiTextFileReader multiTextFileReader = new MultiTextFileReader();
        var texts = await multiTextFileReader.MultiReadAsync(filePaths);
        for(int i=0; i<filePaths.Count; i++)
        {
            var words = fileProcessor.Split(fileProcessor.ToUpper(fileProcessor.RemoveExtraSpace(texts[i])));
            Document d =new Document(Path.GetFileName(filePaths[i]), filePaths[i], texts[i], words);
            documents.Add(d);
        }
        return documents;
    }
}