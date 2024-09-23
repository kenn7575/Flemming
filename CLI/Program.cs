
using BL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

class Program
{
    static async Task Main(string[] args)
    {
        // Create the host builder
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, builder) =>
            {
                // Add appsettings.json configuration
                builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                // Load environment variables to override sensitive values
                builder.AddEnvironmentVariables();
            })
            .ConfigureServices((context, services) =>
            {
                // Register IConfiguration as a service
                services.AddSingleton(context.Configuration);
               
            })
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
            })
            .Build();


     

        // Now you can retrieve the IConfiguration from the built host's service provider
        var configuration = host.Services.GetRequiredService<IConfiguration>();

        // Manually override AzureAd settings with environment variables (if set)
        var tenantId = Environment.GetEnvironmentVariable("AZURE_AD_TENANT_ID");
        var clientId = Environment.GetEnvironmentVariable("AZURE_AD_CLIENT_ID");
        var clientSecret = Environment.GetEnvironmentVariable("AZURE_AD_CLIENT_SECRET");
        var openaiApiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
        var userId = Environment.GetEnvironmentVariable("AZURE_AD_USER_ID");

        if (!string.IsNullOrEmpty(userId))
        {
            configuration["AzureAd:UserId"] = userId;
        }
        if (!string.IsNullOrEmpty(openaiApiKey))
        {
            configuration["OpenAi:ApiKey"] = openaiApiKey;
        }
        if (!string.IsNullOrEmpty(tenantId))
        {
            configuration["AzureAd:TenantId"] = tenantId;
        }

        if (!string.IsNullOrEmpty(clientId))
        {
            configuration["AzureAd:ClientId"] = clientId;
        }

        if (!string.IsNullOrEmpty(clientSecret))
        {
            configuration["AzureAd:ClientSecret"] = clientSecret;
        }

        // Access and use the configuration
        var azureAdConfig = new
        {
            Instance = configuration["AzureAd:Instance"],
            TenantId = configuration["AzureAd:TenantId"],
            ClientId = configuration["AzureAd:ClientId"],
            ClientSecret = configuration["AzureAd:ClientSecret"],
            CallbackPath = configuration["AzureAd:CallbackPath"]
        };

        // Output values for testing (you should avoid printing secrets in a real app)
        Console.WriteLine($"Azure AD Instance: {azureAdConfig.Instance}");
        Console.WriteLine($"Azure AD Tenant ID: {azureAdConfig.TenantId}");
        Console.WriteLine($"Azure AD Client ID: {azureAdConfig.ClientId}");
        Console.WriteLine($"Azure AD Client Secret: (hidden for security)");

        // Your console app logic goes here
        //string new_key = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
        //if ( string.IsNullOrEmpty( new_key))
        //{
        //    Console.WriteLine("OPENAI_API_KEY is not set");
        //    return;
        //}
        //OpenAi openAi = OpenAi.GetInstance(new_key);

        //PilotageInfo pilotageInfo = await openAi.AnalyzePilotageReq(FileManager.ReadFile("files/examples/3/input.txt"));

        //Console.WriteLine(pilotageInfo.ToString());
        //var res = GraphClientProvider.GetNewEmailsAsync();

        //await GraphEmailService.TestCredentials();
        var hopefullyEmails = await GraphEmailService.FetchEmails();

        //var service = new ImapIdleService();
        //await service.ListenForNewEmails();

        Console.ReadKey();


    }
}
