public interface IMultiReader {
    public Task<List<string>> MultiReadAsync(List<string> paths);
}