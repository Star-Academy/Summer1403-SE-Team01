using FullTextSearch.Service.InitializeService;
using FullTextSearch.Service.SearchService;
using Microsoft.AspNetCore.Mvc;

namespace InvertedIndexAPI.Controllers;


[ApiController]
[Route("[controller]/[action]")]
public class SearchController : ControllerBase
{

    private readonly ISearchService _searchService;

    public SearchController(IInitializeServices2 initializeServices2, ISearchService searchService)
    {
        initializeServices2.StartAsync_1();
        _searchService = searchService;
    }


    [HttpGet("{input}")]
    public IActionResult ResponseQuery([FromRoute]string input)
    {
        var result = _searchService.Search(input, InitializeServices2.InvertedIndex);
        return Ok(result.documents);
    }
    
}