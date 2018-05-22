using System.Collections.Generic;

namespace CurrencyConverter.Models
{
    public class Message
    {
        public int Id { get; set; }

        public string Body { get; set; }

        public User Author { get; set; }

        public List<Message> Replies { get; set; }
    }
}