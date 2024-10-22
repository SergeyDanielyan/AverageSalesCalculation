using DataAccess;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
ServiceCollection serviceCollection = new ServiceCollection();
serviceCollection.AddSingleton<IProductService, ProductService>();
var serviceProvider = serviceCollection.BuildServiceProvider();
var productService = serviceProvider.GetRequiredService<IProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/data", (string path) =>
{
    productService.ReadData(path);
}).WithName("PostData").WithOpenApi();

app.MapGet("/ads", (int id) =>
{
    return productService.Ads(id);
}).WithName("GetADS").WithOpenApi();

app.MapGet("/prediction", (int id, int days) =>
{
    return productService.SalesPrediction(id, days);
}).WithName("GetSalesPrediction").WithOpenApi();

app.MapGet("/demand", (int id, int days) =>
{
    return productService.Demand(id, days);
}).WithName("GetDemand").WithOpenApi();

app.Run();
