using FullTextSearch.Core;

namespace FullTextSearch.Controller.QueryController.Abstraction;

public interface IWordCollectorDriver
{
    void DriveCollect(IEnumerable<IWordCollector> collectors, Query query);
}