using System.Runtime.CompilerServices;

public class MultiTextFileReader : IMultiReader
{
    private static MultiTextFileReader instance;

    private MultiTextFileReader() {}

    public static MultiTextFileReader getInstance() {
        if(instance == null) {
            instance = new MultiTextFileReader();
        }
        return instance;
    }
    public async Task<List<string>> MultiReadAsync(List<string> paths) //vr
    {
        var textFileReader = TextFileReader.getInstance();
        var tasks = paths.Select(s=>textFileReader.ReadAsync(s)).ToList();

        var texts = await Task.WhenAll(tasks);
        return texts.ToList();
    }
}