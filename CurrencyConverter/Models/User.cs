using System.Collections.Generic;

namespace CurrencyConverter.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Message> Posts { get; set; } = new List<Message>();
    }
}