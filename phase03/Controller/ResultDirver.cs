using System.Diagnostics;

public class ResultDriver {
    public ResultDriver(List<IResultable> resultables, Result result) {
        foreach(var r in resultables) {
            r.Filter(result);
        }
    }
}