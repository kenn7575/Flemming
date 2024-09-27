using BL.OpenAI;
using OpenAI_API.Moderation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BL
{
    public class CategorizedEmail : Email
    {
        [NotMapped]
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }

        public CategorizedEmail(Email email, string categoryName, int categoryId) : base(email)
        {
            CategoryName = categoryName;
            CategoryId = categoryId;
        }
        public CategorizedEmail(Email email, CategorizeEmailResponse cer ) : base(email)
        {
            CategoryName = cer.CategoryName;
            CategoryId = cer.CategoryId;
        }
        public CategorizedEmail() 
        {
            
        }
    }
}
