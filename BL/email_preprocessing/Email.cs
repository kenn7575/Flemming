using Microsoft.Graph.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Email
    {
        public string? From { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public DateTime? SentDateTime { get; set; }
        public string? ConversationId { get; set; }
        public BodyType? BodyContentType { get; set; }
        public List<Recipient> ReplyTo { get; set; }

        public Email(Message message)
        {
            From = message.From.EmailAddress.Address;
            Subject = message.Subject;
            Body = message.Body.Content;
            BodyContentType = message.Body.ContentType;
            SentDateTime = message.SentDateTime?.DateTime;
            ConversationId = message.ConversationId;
            BodyContentType = message.Body.ContentType;
            ReplyTo = message.ReplyTo;
        }
        public Email(Email message)
        {
            From = message.From;
            Subject = message.Subject;
            Body = message.Body;
            SentDateTime = message.SentDateTime;
            ConversationId = message.ConversationId;
            BodyContentType = message.BodyContentType;
            ReplyTo = message.ReplyTo;
        }
        public void cleanBody()
        {
            if (BodyContentType == BodyType.Html)
            {
                try
                {

                    Body = EmailProcessingTools.CleanEmailBody(Body);
                }
                catch (Exception e)
                {



                }
            }
        }
    }
}

