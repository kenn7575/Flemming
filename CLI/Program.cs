
using BL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Graph.Models;
using System.Text.Json;

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

     
      


        //get emails
        List<BL.Conversation> Conversations = await GraphEmailService.FetchEmails(3);


       


        //convert emails to json and save to file
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        var conversationsJSON = JsonSerializer.Serialize(Conversations);
        string currentTime = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        FileManager.SaveJson($"C:\\Users\\kko\\Downloads\\emails\\{currentTime}.json", conversationsJSON);


        //categorize emails

        //var OpenAI = OpenAi.GetInstance(Environment.GetEnvironmentVariable("OPENAI_API_KEY"));
        //var CategorizedEmails = new  List<CategorizedEmail>();
        //foreach (var email in emails.Value)
        //{
        //    CategorizedEmail ce = await OpenAI.CategorizeEmail(email.Body.Content, email.Subject, email.From.EmailAddress.Address);
        //    ce.Body = email.Body.Content;
        //    ce.Subject = email.Subject;
        //    ce.From = email.From.EmailAddress.Address;


        //    var emailJson = new
        //    {
        //        category = ce.CategoryName,
        //        body = ce.Body,
        //        subject = ce.Subject,
        //        from = ce.From
        //    };

        //    Console.WriteLine($"{ce.Subject} is predicted to be of type {ce.CategoryName}");

        //    //remove "/" from subject
        //    var subject = ce.Subject.Replace("/", "-");
        //    //save to files
        //    switch (ce.CategoryId)
        //    {
        //        case 1:
        //            FileManager.SaveJson($"C:\\Users\\kko\\Downloads\\emails\\pilotage_request\\{subject}.json", JsonSerializer.Serialize(ce));
        //            break;
        //        case 2:
        //            FileManager.SaveJson($"C:\\Users\\kko\\Downloads\\emails\\vessel_arrival_or_departure_notifications\\{subject}.json", JsonSerializer.Serialize(ce));
        //            break;
        //        case 3:
        //            FileManager.SaveJson($"C:\\Users\\kko\\Downloads\\emails\\cargo_operations\\{subject}.json", JsonSerializer.Serialize(ce));
        //            break;
        //        case 4:
        //            FileManager.SaveJson($"C:\\Users\\kko\\Downloads\\emails\\invoice_and_billing_information\\{subject}.json", JsonSerializer.Serialize(ce));
        //            break;
        //        case 5:
        //            FileManager.SaveJson($"C:\\Users\\kko\\Downloads\\emails\\order_cancellations\\{subject}.json", JsonSerializer.Serialize(ce));
        //            break;
        //        case 6:
        //            FileManager.SaveJson($"C:\\Users\\kko\\Downloads\\emails\\order_modifications\\{subject}.json", JsonSerializer.Serialize(ce));
        //            break;
        //        case 7:
        //            FileManager.SaveJson($"C:\\Users\\kko\\Downloads\\emails\\commercialand_sales_inquiries\\{subject}.json", JsonSerializer.Serialize(ce));
        //            break;
        //        case 8:
        //            FileManager.SaveJson($"C:\\Users\\kko\\Downloads\\emails\\spam_and_ads\\{subject}.json", JsonSerializer.Serialize(ce));
        //            break;
        //        case 9:
        //            FileManager.SaveJson($"C:\\Users\\kko\\Downloads\\emails\\miscellaneous\\{subject}.json", JsonSerializer.Serialize(ce));
        //            break;
        //        case 10:
        //            FileManager.SaveJson($"C:\\Users\\kko\\Downloads\\emails\\miscellaneous\\{subject}.json", JsonSerializer.Serialize(ce));
        //            break;
        //        default:
        //            FileManager.SaveJson($"C:\\Users\\kko\\Downloads\\emails\\statement_of_truth\\{subject}.json", JsonSerializer.Serialize(ce));
        //            break;
        //    }
            

        //}
        Console.ReadKey();
    }
}
