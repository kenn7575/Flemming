var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.AddEnvironmentVariables();


var app = builder.Build();

var tenantId = Environment.GetEnvironmentVariable("AZURE_AD_TENANT_ID");
var clientId = Environment.GetEnvironmentVariable("AZURE_AD_CLIENT_ID");
var clientSecret = Environment.GetEnvironmentVariable("AZURE_AD_CLIENT_SECRET");
var openaiApiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
if(!string.IsNullOrEmpty(openaiApiKey))
{
    builder.Configuration["OpenAI:ApiKey"] = openaiApiKey;
}
if (!string.IsNullOrEmpty(tenantId))
{
builder.Configuration["AzureAd:TenantId"] = tenantId;
}

if (!string.IsNullOrEmpty(clientId))
{
builder.Configuration["AzureAd:ClientId"] = clientId;
}

if (!string.IsNullOrEmpty(clientSecret))
{
    builder.Configuration["AzureAd:ClientSecret"] = clientSecret;

    // Access the configuration and ensure values are set
    var azureAdConfig = new
    {
        Instance = builder.Configuration["AzureAd:Instance"],
        TenantId = builder.Configuration["AzureAd:TenantId"],
        ClientId = builder.Configuration["AzureAd:ClientId"],
        ClientSecret = builder.Configuration["AzureAd:ClientSecret"],
        CallbackPath = builder.Configuration["AzureAd:CallbackPath"]
    };

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
}
app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
