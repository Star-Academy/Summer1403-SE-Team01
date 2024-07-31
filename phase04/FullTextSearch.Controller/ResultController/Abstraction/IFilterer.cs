using FullTextSearch.Core;

namespace FullTextSearch.Controller.ResultController;

public interface IFilterer {
    public void Filter(Result result);
}