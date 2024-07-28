using System.Diagnostics;
using System.Runtime.CompilerServices;

public class FiltererDirver {
    public void DriveFilterer(IEnumerable<IFilterer> filterers, Result result)
    {
        filterers.ToList().ForEach(r=>r.Filter(result));
    }
}