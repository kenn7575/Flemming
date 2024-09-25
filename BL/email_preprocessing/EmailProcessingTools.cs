using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BL
{
    public class EmailProcessingTools
    {

        public static string CleanEmailBody(string htmlContent)
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
