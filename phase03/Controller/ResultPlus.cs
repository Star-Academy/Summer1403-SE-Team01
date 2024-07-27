public class ResultPlus : IResultable
{
    public void Filter(Result result)
    {
        result.documents = result.documents.Intersect(result.map['+']).ToList();
    }
}