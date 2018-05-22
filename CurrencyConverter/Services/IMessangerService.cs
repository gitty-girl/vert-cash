using System.Threading.Tasks;
using CurrencyConverter.ExecutionControl;
using CurrencyConverter.Models;
using CurrencyConverter.Models.Dtos;

namespace CurrencyConverter.Services
{
    public interface IMessangerService
    {
        Task<ExecutionResult<Message>> GetMessageAsync(int id);

        Task<ExecutionResult> PostMessageAsync(MessageDto dto);

        Task<ExecutionResult> EditMessageAsync(int id, MessageDto message);

        Task<ExecutionResult> DeleteMessageAsync(int id);
    }
}