using ProviderOne.Contracts;
using ProviderOne.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddScoped<IDataService, DataService>();

var app = builder.Build();

app.MapControllers();

app.Run();