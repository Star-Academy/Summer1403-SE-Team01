using FullTextSearch.Core;

namespace FullTextSearch.Service.InitializeService;

public interface IInitializeService
{
    public Task<Dictionary<string, IEnumerable<Document>>> Initialize(string directoryPath);
}