public class MultiTextFileReader : IMultiReader
{
    public async Task<List<string>> MultiReadAsync(List<string> paths) 
    {
        var textFileReader = new TextFileReader();
        var tasks = paths.Select(path=>textFileReader.ReadAsync(path)).ToList();

        var texts = await Task.WhenAll(tasks);
        return texts.ToList();
    }
}