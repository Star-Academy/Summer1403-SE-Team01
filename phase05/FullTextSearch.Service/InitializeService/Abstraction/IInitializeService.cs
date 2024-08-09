using FullTextSearch.Core;

namespace FullTextSearch.Service.InitializeService.Abstraction;

public interface IInitializeService
{
    Task<Dictionary<string, IEnumerable<Document>>> Initialize(string directoryPath);
}