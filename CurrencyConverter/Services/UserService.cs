using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CurrencyConverter.ExecutionControl;
using CurrencyConverter.Infrastructure;
using CurrencyConverter.Models;
using CurrencyConverter.Models.Dtos;
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

        public async Task<ExecutionResult<User>> GetUserAsync(int id)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

                if (user == null)
                    throw new Exception("User not found");

                return ExecutionResult<User>.Success(user);
            }
            catch (Exception ex)
            {
                return new ExecutionResult<User>(ExecutionResult.DomainFailedResult(new Dictionary<string, string>
                {
                    { "Bad Request", ex.Message }
                }));
            }
        }

        public async Task<ExecutionResult> AddUserAsync(UserDto dto)
        {
            try
            {
                var user = User.Factory.CreateNew(dto);

                await _context.AddAsync(user);
                await _context.SaveChangesAsync();

                return ExecutionResult.Success();
            }
            catch (Exception ex)
            {
                return ExecutionResult.DomainFailedResult(new Dictionary<string, string>
                {
                    { "Bad Request", ex.Message }
                });
            }
        }

        public async Task<ExecutionResult> UpdateUserAsync(int id, UserDto dto)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

                if (user == null)
                    throw new Exception("User not found");

                user.Update(dto);
                _context.Users.Update(user);

                await _context.SaveChangesAsync();

                return ExecutionResult.Success();
            }
            catch (Exception ex)
            {
                return ExecutionResult.DomainFailedResult(new Dictionary<string, string>
                {
                    { "Bad Request", ex.Message }
                });
            }
        }

        public async Task<ExecutionResult> DeleteUser(int id)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

                if (user == null)
                    throw new Exception("User not found");

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                return ExecutionResult.Success();
            }
            catch (Exception ex)
            {
                return ExecutionResult.DomainFailedResult(new Dictionary<string, string>
                {
                    { "Bad Request", ex.Message }
                });
            }
        }

        public async Task<ExecutionResult> Seed()
        {
            var authorCount = await _context.Users.CountAsync();

            if (authorCount == 0)
                foreach (var user in GetDefaultUsers())
                    await AddUserAsync(user);

            return ExecutionResult.Success();
        }

        private static IEnumerable<UserDto> GetDefaultUsers() =>
            new List<UserDto>
            {
                new UserDto{ Name = "Mariam ", Surname = "Tchanturia" },
                new UserDto{ Name = "Magda", Surname = "Tsintsadze" }
            };
    }
}