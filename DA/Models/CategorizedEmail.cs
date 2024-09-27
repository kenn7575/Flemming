using Microsoft.Graph.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Models
{
    public class CategorizedEmail
    {
        [Key]
        public int ID { get; set; }
        public string? From { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public DateTime? SentDateTime { get; set; }
        public string? ConversationId { get; set; }
        public BodyType? BodyContentType { get; set; }
        public ICollection<string>? ReplyTo { get; set; }
        public int CategoryId { get; set; }

    }
}
