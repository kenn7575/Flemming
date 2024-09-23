using Microsoft.Extensions.Configuration;


using Azure.Identity;
using Microsoft.Graph;
using Microsoft.Graph.Models;
namespace BL
{
    public class GraphClientProvider
    {
        private readonly IConfiguration _config;

        public GraphClientProvider(IConfiguration config)
        {
            _config = config;
        }

        public GraphServiceClient GetGraphClient()
        {
            // Use ClientSecretCredential for app-only authentication
            var clientSecretCredential = new ClientSecretCredential(
                _config["AzureAd:TenantId"],
                _config["AzureAd:ClientId"],
                _config["AzureAd:ClientSecret"]
            );

            // Create the GraphServiceClient with the Azure.Identity token credential
            var graphClient = new GraphServiceClient(clientSecretCredential,
                new[] { "https://graph.microsoft.com/.default" });

            return graphClient;
        }

        public static async Task<IEnumerable<Message>?> GetNewEmailsAsync(GraphServiceClient graphClient)
        {
            var messagesResponse = await graphClient.Me.Messages.GetAsync();
            return messagesResponse?.Value;
        }

    }
}