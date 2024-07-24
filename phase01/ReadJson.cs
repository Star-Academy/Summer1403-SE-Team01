using System.Collections.Generic;
using System.IO;
using System.Resources;
using System.Text.Json;
using System.Threading.Tasks;
using System.Reflection;
namespace ScoreBoard
{
    public class ReadJson : IReadData
    {
        public async Task<List<T>> ReadAsync<T>(string path)
        {
            var resourceManager = new ResourceManager($"project.{path}", Assembly.GetExecutingAssembly());
            var jsonData = resourceManager.GetString(path);
            return JsonSerializer.Deserialize<List<T>>(jsonData);
        }
    }
}