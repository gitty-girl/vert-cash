using System;
using System.Collections.Generic;
using CurrencyConverter.Models.Dtos;

namespace CurrencyConverter.Models
{
    public class Message
    {
        public int Id { get; set; }

        public string Body { get; private set; }

        public virtual User Author { get; private set; }

        public int AuthorId { get; private set; }

        public virtual List<Message> Replies { get; private set; }

        private Message()
        {
        }

        private Message(MessageDto dto)
        {
            ValidateCreation(dto);

            Body = dto.Body;
            AuthorId = dto.AuthorId;
        }

        public void Update(MessageDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Body))
                throw new Exception("Message text can not be empty");

            Body = dto.Body;
        }

        public void Reply(Message message)
        {
            if (Replies == null)
                Replies = new List<Message>();

            Replies.Add(message);
        }

        private void ValidateCreation(MessageDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Body))
                throw new Exception("Message text can not be empty");
            if (dto.AuthorId == 0)
                throw new Exception("Invalid author id");
        }

        public static class Factory
        {
            public static Message CreateNew(MessageDto dto) =>
                new Message(dto);
        }
    }
}