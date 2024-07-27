public class Driver {
    public Driver(IEnumerable<ISignable> signables, Query query, Result result, Dictionary<string,List<Document>> dictionary) {
        foreach(var s in signables) {
            result.map.Add(s.sign, s.Extract(query, dictionary));
        }
    }
}