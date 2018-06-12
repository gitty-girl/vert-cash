using System.Threading.Tasks;
using CurrencyConverter.ExecutionControl;
using CurrencyConverter.Models;
using CurrencyConverter.Models.Dtos;

namespace CurrencyConverter.Services
{
    public interface IUserService
    {
        Task<ExecutionResult<User>> GetUserAsync(int id);

        Task<ExecutionResult> AddUserAsync(UserDto dto);

        Task<ExecutionResult> UpdateUserAsync(int id, UserDto dto);

        Task<ExecutionResult> DeleteUser(int id);

        Task<ExecutionResult> Seed();
    }
}