using ProviderTwo.Contracts;
using ProviderTwo.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddScoped<IDataService, DataService>();

var app = builder.Build();

app.MapControllers();

app.Run();