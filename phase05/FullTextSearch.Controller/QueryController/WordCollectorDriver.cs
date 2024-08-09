using FullTextSearch.Controller.QueryController.Abstraction;
using FullTextSearch.Core;

namespace FullTextSearch.Controller.QueryController;

public class WordCollectorDriver : IWordCollectorDriver
{
    public void DriveCollect(IEnumerable<IWordCollector> collectors, Query query)
    {
        collectors.ToList().ForEach(c => 
            query.WordsBySign[c.Sign] = c.RemovePrefix(c.Collect(query.Text)));
    }
}