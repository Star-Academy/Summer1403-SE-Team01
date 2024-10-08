public class ResultHandler {
    public void Prepare(Result result, bool flag)
    {
        RemoveMinusWords(result);
        RemoveNotOptionalWords(result, flag);
    }

    private void RemoveMinusWords(Result result)
    {
        result.documents  = result.OrdinaryDocuments.Except(result.MinesDocuments).ToList();
    }

    private void RemoveNotOptionalWords(Result result, bool hasPlusWord) 
    {
        if(hasPlusWord)
            result.documents = result.documents.Intersect(result.plusDocuments).ToList();
    } 

}