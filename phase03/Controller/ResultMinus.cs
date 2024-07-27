public class ResultMinus : IResultable
{
    public void Filter(Result result)
    {
        result.documents = result.documents.Except(result.map['-']).ToList();
    }
}