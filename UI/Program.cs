using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IEnumerable<string>? initialScopes = builder.Configuration["DownstreamApi:Scopes"]?.Split(' ');



builder.Services.AddMicrosoftIdentityWebAppAuthentication(builder.Configuration, "AzureAd")
    .EnableTokenAcquisitionToCallDownstreamApi(initialScopes)
        .AddDownstreamApi("GraphApi", builder.Configuration.GetSection("GraphApi"))
        .AddInMemoryTokenCaches();

builder.Services.AddControllersWithViews()
                .AddMicrosoftIdentityUI();

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = options.DefaultPolicy;
});

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor()
                .AddMicrosoftIdentityConsentHandler();

//env variables
builder.Configuration.AddEnvironmentVariables();

// Add services to the container.
WebApplication app = builder.Build();

//overwrite configuration with env variables
var tenantId = Environment.GetEnvironmentVariable("AZURE_AD_TENANT_ID");
var clientId = Environment.GetEnvironmentVariable("AZURE_AD_CLIENT_ID");
var clientSecret = Environment.GetEnvironmentVariable("AZURE_AD_CLIENT_SECRET");

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
}
    // Access the configuration and ensure values are set
    var azureAdConfig = new
    {
        Instance = builder.Configuration["AzureAd:Instance"],
        TenantId = builder.Configuration["AzureAd:TenantId"],
        ClientId = builder.Configuration["AzureAd:ClientId"],
        ClientSecret = builder.Configuration["AzureAd:ClientSecret"],
        CallbackPath = builder.Configuration["AzureAd:CallbackPath"]
    };

    Console.WriteLine($"Azure AD Instance: {azureAdConfig.Instance}");
    Console.WriteLine($"Azure AD Tenant ID: {azureAdConfig.TenantId}");
    Console.WriteLine($"Azure AD Client ID: {azureAdConfig.ClientId}");
    Console.WriteLine($"Azure AD Client Secret: (hidden for security)");

    app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapBlazorHub();
    endpoints.MapFallbackToPage("/_Host");
});

app.Run();
