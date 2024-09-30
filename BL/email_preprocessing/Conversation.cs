using BL.OpenAI;
using OpenAI_API.Moderation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Conversation
    {
        private List<Email> _messages;

        public List<Email> Messages
        {
            get => _messages;
            private set
            {
                _messages = value;
                SortMessages(); // Ensure list is sorted when updated
            }
        }

        public Conversation()
        {
            _messages = new List<Email>();
        }

        public Conversation(Conversation conversation)
        {
           this._messages = new List<Email>(conversation.Messages);
        }
        public Conversation(List<Email> messages)
        {
            _messages = new List<Email>(messages);
            SortMessages(); // Sort the list during initialization
        }

        // Adds an email and automatically sorts the list
        public void AddEmail(Email email)
        {
            _messages.Add(email);
            SortMessages();
        }

        // Removes an email and automatically sorts the list
        public void RemoveEmail(Email email)
        {
            _messages.Remove(email);
            SortMessages();
        }

        // Private method to sort the messages by SentDateTime
        private void SortMessages()
        {
            //if any of the messages have a null SentDateTime, don't sort
            if (_messages.Any(x => !x.SentDateTime.HasValue))
            {
                return;
            }
            _messages = _messages
                .Where(x => x.SentDateTime.HasValue)  // Make sure emails have a valid SentDateTime
                .OrderBy(x => x.SentDateTime.Value)   // Sort by SentDateTime (oldest first)
                .ToList();
        }
    }
    public class CategorizedConversation : Conversation
    {
      
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public CategorizedConversation(Conversation conversation, int categoryId) : base(conversation)
        {
            categoryId = CategoryId;
        }
        public CategorizedConversation(Conversation conversation, CategorizeEmailResponse category) : base(conversation)
        {
            CategoryId = category.CategoryId;
            CategoryName = category.CategoryName;
        }
        public void testc()
        {
            var test = new Conversation();
          
        }
    }
}
