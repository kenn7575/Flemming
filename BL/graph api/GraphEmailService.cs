using Azure.Identity;

using HtmlAgilityPack;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using OpenAI_API.Chat;
using System.Net.Http;
using System.Text.Json;
using System.Text.RegularExpressions;

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

        public static async Task<List<Conversation>> FetchEmails(int top)
        {
            var graphClient = GetGraphClient();

            // Step 1: Fetch the initial messages
            var messages = await graphClient.Users["OperationMailFlow-Test@danpilot.dk"].Messages
            .GetAsync((requestConfiguration) =>
            {
                // Set request headers
                requestConfiguration.Headers.Add("Prefer", "outlook.body-content-type=\"text\"");
                requestConfiguration.QueryParameters.Top = top;

                // Fetch only the conversationId for each message
                requestConfiguration.QueryParameters.Select = new string[] { "conversationId" };
            });

            List<Conversation> conversations = new();

            // Step 2: For each message, fetch the entire conversation
            if (messages.Value.Count > 0)
            {
                foreach (var message in messages.Value)
                {
                    // Get the conversationId for the current message (not always the first one)
                    var conversationId = message.ConversationId;

                    // Create a new conversation object for each thread
                    Conversation conversation = new();

                    // Step 3: Fetch all messages in the conversation
                    var conversationMessages = await graphClient.Users["OperationMailFlow-Test@danpilot.dk"].Messages
                        .GetAsync((requestConfiguration) =>
                        {
                            requestConfiguration.QueryParameters.Select = new string[] { "subject", "bodyPreview", "body", "from", "toRecipients", "receivedDateTime","ccRecipients", "sentDateTime", "replyTo",   "id" };
                            requestConfiguration.QueryParameters.Filter = $"conversationId eq '{conversationId}'";
                            requestConfiguration.QueryParameters.Filter = $"conversationId eq '{conversationId}'";
                        });

                    // Step 4: Add all messages in the conversation to the current conversation object
                    foreach (var conversationMessage in conversationMessages.Value)
                    {
                      
                            Email email = new(conversationMessage);
                            email.cleanBody();
                            conversation.AddEmail(email);
                       
                    }

                    // Add the populated conversation to the list
                    conversations.Add(conversation);
                }
            }

            return conversations;
        }







        private static string CleanEmailBody(string htmlContent)
        {
            // Step 1: Decode Unicode escapes like \u0022 to actual characters
            var decodedContent = Regex.Unescape(htmlContent);

            // Step 2: Load the HTML content into HtmlAgilityPack
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(decodedContent);

            // Step 3: Remove all attributes from HTML nodes (like style, class, etc.)
            foreach (var node in doc.DocumentNode.SelectNodes("//*"))
            {
                node.Attributes.RemoveAll();
            }

            // Step 4: Extract the cleaned plain text from the HTML content
            return doc.DocumentNode.InnerText;
        }
    }
}

