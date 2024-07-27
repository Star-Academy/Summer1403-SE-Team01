public class ResultHandler {
    public void Prepare(Result result)
    {
        RemoveMinusWords(result);
        RemoveNotOptionalWords(result);
    }

    private void RemoveMinusWords(Result result)
    {
        result.documents  = result.map['+'].Except(result.map['-']).ToList();
    }

    private void RemoveNotOptionalWords(Result result) 
    {
        result.documents = result.documents.Intersect(result.map[' ']).ToList();
    } 

}