using System.Runtime.CompilerServices;

public class MultiTextFileReader : IMultiReader
{
    public async Task<List<string>> MultiReadAsync(List<string> paths) //vr
    {
        TextFileReader textFileReader = new TextFileReader();
        var tasks = paths.Select(s=>textFileReader.ReadAsync(s)).ToList();

        var texts = await Task.WhenAll(tasks);
        return texts.ToList();
    }
}