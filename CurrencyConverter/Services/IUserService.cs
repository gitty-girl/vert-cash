using System.Threading.Tasks;
using CurrencyConverter.ExecutionControl;

namespace CurrencyConverter.Services
{
    public interface IUserService
    {
        Task<ExecutionResult> Seed();
    }
}