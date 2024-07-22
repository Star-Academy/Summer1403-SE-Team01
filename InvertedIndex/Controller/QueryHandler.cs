public class QueryHandler {

    //public Document document { get; set; }
    //clear useless
    //split
    //upercase
    //set->list


    public void Prepare(Query query)
    {
        //+
        //-
        //split
        //upper
        var temp = query.query;
        query.query = UpperText(temp);
    }
    public string UpperText(string text)
    {
        var uppered = text.ToUpper();
        return uppered;
    }

}