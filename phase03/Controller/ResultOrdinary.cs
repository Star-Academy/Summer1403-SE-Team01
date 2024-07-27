public class ResultOrdinary : IResultable
{
    public void Filter(Result result)
    {
        result.documents = result.map[' '];
    }
}