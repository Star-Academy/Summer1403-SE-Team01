public class ResultPlus : IResultable
{
    public void Filter(Result result)
    {
        result.documents = result.map[' '].Intersect(result.map['+']).ToList();
    }
}