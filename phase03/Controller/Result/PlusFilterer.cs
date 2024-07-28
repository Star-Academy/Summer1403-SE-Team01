public class PlusFilterer : IFilterer
{
    public void Filter(Result result)
    {
        result.documents = result.documents.Intersect(result.signToDocumentListDictionary['+']).ToList();
    }
}