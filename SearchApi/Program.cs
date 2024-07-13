using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using SearchApi.Contracts;
using SearchApi.Factories;
using SearchApi.Models.Settings;
using SearchApi.Profiles;
using SearchApi.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddHttpClient();
builder.Services.AddMemoryCache();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection(nameof(AppSettings)));
builder.Services.AddAutoMapper(typeof(ProviderProfile));

builder.Services.AddScoped<IRoutesRepository, RoutesRepository>();
builder.Services.AddScoped<ProviderOneService>();
builder.Services.AddScoped<ProviderTwoService>();

builder.Services.AddScoped<ISearchService, SearchService>();

builder.Services.AddScoped<IProviderFactory, ProviderFactory>();

var app = builder.Build();
app.MapControllers();

app.Run();

