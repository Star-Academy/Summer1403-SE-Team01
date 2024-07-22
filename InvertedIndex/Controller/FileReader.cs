public class FileReader : IDataReader
{
    public async Task<string> Read(string path)
    {
        var text = await File.ReadAllTextAsync(path);
        return text;
    }
}