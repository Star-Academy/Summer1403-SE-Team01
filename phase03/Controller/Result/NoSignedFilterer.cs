public class NoSignedFilterer : IFilterer
{
    public void Filter(Result result)
    {
        result.documents = result.signToDocumentListDictionary[' '];
    }
}