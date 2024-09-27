using Microsoft.Graph.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Email
    {
        [Key]
        public int ID { get; set; }
        public string? From { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public DateTime? SentDateTime { get; set; }
        public string? ConversationId { get; set; }
        public BodyType? BodyContentType { get; set; }
        public List<string>? ReplyTo { get; set; }

        public Email(Message message)
        {
            From = message.From.EmailAddress.Address;
            Subject = message.Subject;
            Body = message.Body.Content;
            SentDateTime = message.SentDateTime?.DateTime;
            ConversationId = message.ConversationId;
            BodyContentType = message.Body.ContentType;

            //convert the list of email addresses to a list of strings
            ReplyTo = new List<string>();
            foreach (Recipient email in message.ReplyTo)
            {
                ReplyTo.Add(email.EmailAddress.Address);
            }
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
        public Email()
        {
           
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

