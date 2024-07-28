interface IReader
{
    public Task<string> ReadAsync(string path);
}