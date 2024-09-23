using Azure.Identity;
using Microsoft.Graph;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using static System.Formats.Asn1.AsnWriter;
using Microsoft.Graph.Models;

namespace BL
{


    public class GraphEmailService
    {
        private static GraphServiceClient GetGraphClient()
        {
            // Multi-tenant apps can use "common",
            // single-tenant apps must use the tenant ID from the Azure portal
            var tenantId = Environment.GetEnvironmentVariable("AZURE_AD_TENANT_ID");

            // Value from app registration
            var clientId = Environment.GetEnvironmentVariable("AZURE_AD_CLIENT_ID");

            // Value from app registration (Client Secret)
            var clientSecret = Environment.GetEnvironmentVariable("AZURE_AD_CLIENT_SECRET");

            // Use ClientSecretCredential for backend services
            var clientSecretCredential = new ClientSecretCredential(
                tenantId, clientId, clientSecret);

            // Define the necessary scopes for the application
            var scopes = new[] { "https://graph.microsoft.com/.default" };

            // Create the GraphServiceClient using the credential
            var graphClient = new GraphServiceClient(clientSecretCredential, scopes);

            return graphClient;
        }

        public static async Task<MessageCollectionResponse> FetchEmails()
        {
            var graphClient = GetGraphClient();
            var messages = await graphClient.Users["OperationMailFlow-Test@danpilot.dk"].Messages
            .GetAsync();

            //return a list of messages
            return messages;
        }
    }

    // Call the FetchEmails method



}

