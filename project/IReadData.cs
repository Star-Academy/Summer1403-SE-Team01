namespace ScoreBoard
{
    public interface IReadData {
        public Task<List<T>> ReadAsync<T>(string path);
    }
}