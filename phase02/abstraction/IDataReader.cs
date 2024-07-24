interface IDataReader
{
    public Task<string> ReadAsync(string path);
}