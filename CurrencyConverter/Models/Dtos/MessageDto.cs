namespace CurrencyConverter.Models.Dtos
{
    public class MessageDto
    {
        public int? ParentMessageId { get; set; }

        public string Body { get; set; }

        public int AuthorId { get; set; }
    }
}