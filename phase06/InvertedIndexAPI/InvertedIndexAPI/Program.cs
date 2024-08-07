using FullTextSearch.Controller.QueryController;
using FullTextSearch.Controller.QueryController.Abstraction;
using FullTextSearch.Controller.ResultController;
using FullTextSearch.Controller.ResultController.Abstraction;
using FullTextSearch.Controller.SearchController;
using FullTextSearch.Controller.SearchController.Abstraction;
using FullTextSearch.Controller.TextFormatter;
using FullTextSearch.Controller.TextFormatter.Abstraction;
using FullTextSearch.Service.InitializeService;
using FullTextSearch.Service.SearchService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();



//builder.Services.AddSingleton<>();


builder.Services.AddScoped<ITextFormatter, TextFormatter>();
builder.Services.AddScoped<IQueryFormatter, QueryFormatter>();
builder.Services.AddScoped<IQueryBuilder, QueryBuilder>();
builder.Services.AddScoped<IFilterDriver, FilterDriver>();
builder.Services.AddScoped<ISearcherDriver, SearcherDriver>();
builder.Services.AddScoped<IResultBuilder, ResultBuilder>();
builder.Services.AddScoped<ISearchService, SearchService>();


//builder.Services.AddScoped<IServiceProvider, ServiceProvider>();
builder.Services.AddSingleton<IInitializeServices2, InitializeServices2>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

