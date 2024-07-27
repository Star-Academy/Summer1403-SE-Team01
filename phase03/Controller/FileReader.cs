public class FileReader : IDataReader
{
    public async Task<string> ReadAsync(string path)
    {
        var text = await File.ReadAllTextAsync(path);
        return text;
    }
}