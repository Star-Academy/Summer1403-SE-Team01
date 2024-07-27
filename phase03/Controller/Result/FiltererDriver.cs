using System.Diagnostics;

public class FiltererDirver {
    public FiltererDirver(IEnumerable<IFilterer> resultables, Result result) {
        foreach(var r in resultables) {
            r.Filter(result);
        }
    }
}