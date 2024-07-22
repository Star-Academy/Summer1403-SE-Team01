    public class SearchHandler 
    {
        public Dictionary<string, List<Document>> dic{ get; set;}
        public QueryHandler queryHandler;
        public SearchHandler(Dictionary<string, List<Document>> dic)
        {
            this.dic = dic;
            queryHandler = new QueryHandler();
        }

        public Search search(string input)
        {
            Query query = new Query(input);
            queryHandler.Prepare(query);

            Search search = new Search(query);

            var listAnswer = dic[search.query.query];
            Result result = new Result(listAnswer);
            search.result = result;
            return search;
        }



}