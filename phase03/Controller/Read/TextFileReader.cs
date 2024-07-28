public class TextFileReader : IReader
{
    private static TextFileReader instance;

    private TextFileReader() {}

    public static TextFileReader getInstance() {
        if(instance == null) {
            instance = new TextFileReader();
        }
        return instance;
    }

    public async Task<string> ReadAsync(string path)
    {
        var text = await File.ReadAllTextAsync(path);
        return text;
    }
}