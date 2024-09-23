using MailKit.Net.Imap;
using MailKit;
using System;
using Azure.Identity; // For token acquisition
using System.Threading.Tasks;
using Azure.Core;
using System.Net.Mail;
using MailKit.Security;

public class ImapIdleService
{
    private static async Task<string> GetAccessTokenAsync()
    {
        var tenantId = Environment.GetEnvironmentVariable("AZURE_AD_TENANT_ID");
        var clientId = Environment.GetEnvironmentVariable("AZURE_AD_CLIENT_ID");
        var clientSecret = Environment.GetEnvironmentVariable("AZURE_AD_CLIENT_SECRET");

        var clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);

        // Request an access token for IMAP
        var tokenRequestContext = new TokenRequestContext(new[] { "https://outlook.office365.com/.default" });
        var token = await clientSecretCredential.GetTokenAsync(tokenRequestContext);

        return token.Token;
    }

    public async Task ListenForNewEmails()
    {
        var accessToken = await GetAccessTokenAsync();

        using (var client = new ImapClient())
        {
            client.Connect("Mail.danpilot.dk", 993, true);

            // Use the OAuth 2.0 access token for authentication
            var oauth2 = new SaslMechanismOAuth2("OperationMailFlow-Test@danpilot.dk", accessToken);
            client.Authenticate(oauth2);

            var inbox = client.Inbox;
            inbox.Open(FolderAccess.ReadOnly);

            inbox.CountChanged += (sender, e) =>
            {
                Console.WriteLine($"New email arrived: {inbox.Count} messages");
            };

            // Listen for new messages
            using (var doneTokenSource = new CancellationTokenSource())
            {
                client.Idle(doneTokenSource.Token);
            }
            client.Disconnect(true);
        }
    }
}

// Usage

