public interface IQueryProcessor : IProcessor
{
    public List<string> SeparatePrefix(List<string> input);
    public List<string> ExtractBySign(List<string> list, char c);

}