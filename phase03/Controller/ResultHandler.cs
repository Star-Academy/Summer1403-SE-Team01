public class ResultHandler {
    public void Prepare(Result result)
    {
        RemoveMinusWords(result);
        RemoveNotOptionalWords(result);
    }

    private void RemoveMinusWords(Result result)
    {
        result.documents  = result.OrdinaryDocuments.Except(result.MinesDocuments).ToList();
    }

    private void RemoveNotOptionalWords(Result result) 
    {
        result.documents = result.documents.Intersect(result.plusDocuments).ToList();
    } 

}