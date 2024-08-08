using FullTextSearch.Core;

namespace FullTextSearch.Service.InitializeService;

public interface IInitializeService
{
    Dictionary<string, IEnumerable<Document>> Initialize(string directoryPath);
}