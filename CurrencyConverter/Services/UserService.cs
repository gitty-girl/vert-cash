using System.Collections.Generic;
using System.Threading.Tasks;
using CurrencyConverter.ExecutionControl;
using CurrencyConverter.Infrastructure;
using CurrencyConverter.Models;
using Microsoft.EntityFrameworkCore;

namespace CurrencyConverter.Services
{
    public class UserService : IUserService 
    {
        private readonly LocalContext _context;

        public UserService(LocalContext context)
        {
            _context = context;
        }

        public async Task<ExecutionResult> Seed()
        {
            var authorCount = await _context.Users.CountAsync();

            if (authorCount == 0)
            {
                await _context.AddRangeAsync(GetDefaultUsers());
                await _context.SaveChangesAsync();
            }

            return ExecutionResult.Success();
        }

        private static IEnumerable<User> GetDefaultUsers() =>
            new List<User>
            {
                new User
                {
                    Name = "Mariam Tchanturia"
                },
                new User
                {
                    Name = "Magda Tsintsadze"
                }
            };
    }
}