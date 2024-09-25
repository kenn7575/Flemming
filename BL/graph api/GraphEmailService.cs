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


            // Step 1: Fetch top messages from the Inbox
            var inboxMessages = await graphClient.Users["OperationMailFlow-Test@danpilot.dk"].MailFolders["inbox"].Messages
                .GetAsync((requestConfiguration) =>
                {
                    requestConfiguration.Headers.Add("Prefer", "outlook.body-content-type=\"text\"");
                    requestConfiguration.QueryParameters.Top = top;
                    requestConfiguration.QueryParameters.Select = new string[] { "conversationId", "subject", "receivedDateTime", "from", "toRecipients", "ccRecipients", "id" };
                });

            List<Conversation> conversations = new();

            // Step 2: For each message, get its conversation and add only relevant replies
            foreach (var message in inboxMessages.Value)
            {
                // Create a new conversation object for the current message's thread
                var conversationId = message.ConversationId;
                Conversation conversation = new();

                // Step 3: Fetch all messages in the conversation (thread) from the Inbox
                var threadMessages = await graphClient.Users["OperationMailFlow-Test@danpilot.dk"].Messages
                    .GetAsync((requestConfiguration) =>
                    {
                        requestConfiguration.QueryParameters.Filter = $"conversationId eq '{conversationId}'";
                        requestConfiguration.QueryParameters.Select = new string[] { "subject", "body", "from", "toRecipients", "ccRecipients", "receivedDateTime", "id" };
                    });

                // Step 4: Add inbox messages to the conversation
                foreach (var threadMessage in threadMessages.Value)
                {
                    Email email = new(threadMessage);
                    email.cleanBody();
                    conversation.AddEmail(email);
                }

                // Step 5: Fetch related sent messages and add them to the same conversation
                var sentMessages = await graphClient.Users["OperationMailFlow-Test@danpilot.dk"].MailFolders["sentitems"].Messages
                    .GetAsync((requestConfiguration) =>
                    {
                        requestConfiguration.QueryParameters.Filter = $"conversationId eq '{conversationId}'";
                        requestConfiguration.QueryParameters.Select = new string[] { "subject", "body", "from", "toRecipients", "ccRecipients", "sentDateTime", "id" };
                    });

                // Step 6: Add sent messages to the conversation
                foreach (var sentMessage in sentMessages.Value)
                {
                    Email email = new(sentMessage);
                    email.cleanBody();
                    conversation.AddEmail(email);
                }

                // Add the conversation to the list
                conversations.Add(conversation);
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

