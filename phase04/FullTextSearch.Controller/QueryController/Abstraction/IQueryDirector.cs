namespace FullTextSearch.Controller.QueryController.Abstraction;

public interface IQueryDirector
{
    public void Construct(IQueryBuilder queryBuilder);
}