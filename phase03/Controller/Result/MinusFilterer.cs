public class MinusFilterer : IFilterer
{
    public void Filter(Result result)
    {
        result.documents = result.documents.Except(result.map['-']).ToList();
    }
}