using System;
using System.Collections.Generic;
using CurrencyConverter.Models.Dtos;

namespace CurrencyConverter.Models
{
    public class User
    {
        public int Id { get; set; }

        public string FullName { get; private set; }

        public DateTime DateCreated { get; private set; }

        public List<Message> Posts { get; set; } = new List<Message>();

        private User()
        {
        }

        private User(UserDto dto)
        {
            ValidateCreation(dto);

            FullName = dto.Name + ' ' + dto.Surname;
            DateCreated = DateTime.Now;
        }

        public void Update(UserDto dto)
        {
            ValidateCreation(dto);

            FullName = dto.Name + ' ' + dto.Surname;
        }

        private void ValidateCreation(UserDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new Exception("User name cannot be empty");

            if (string.IsNullOrWhiteSpace(dto.Surname))
                throw new Exception("User surname cannot be empty");

            dto.Name = dto.Name.Trim();
            dto.Surname = dto.Surname.Trim();
        }

        public static class Factory
        {
            public static User CreateNew(UserDto dto) =>
                new User(dto);
        }
    }
}