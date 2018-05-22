using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConverter.ExecutionControl
{
    public static class ExecutionResultExtensions
    {
        public static IActionResult ToHttpResponse<T>(this ExecutionResult<T> executionResult)
        {
            switch (executionResult.ExecutionResultType)
            {
                case ExecutionResultType.Success:
                    return new ObjectResult(executionResult.Result) { StatusCode = (int)HttpStatusCode.OK };
                case ExecutionResultType.DomainError:
                case ExecutionResultType.SystemError:
                    return new ObjectResult(executionResult.GetExecutionErrors()) { StatusCode = (int)HttpStatusCode.BadRequest };
                default:
                    return new ObjectResult(executionResult.GetExecutionErrors().First().Value) { StatusCode = (int)HttpStatusCode.InternalServerError };
            }
        }

        public static async Task<IActionResult> ToHttpResponseAsync<T>(this Task<ExecutionResult<T>> executionResultAsync)
        {
            var executionResult = await executionResultAsync;

            return ToHttpResponse(executionResult);
        }

        public static IActionResult ToHttpResponse(this ExecutionResult executionResult)
        {
            switch (executionResult.ExecutionResultType)
            {
                case ExecutionResultType.Success:
                    return new StatusCodeResult((int)HttpStatusCode.OK);
                case ExecutionResultType.DomainError:
                case ExecutionResultType.SystemError:
                    return new ObjectResult(executionResult.GetExecutionErrors()) { StatusCode = (int)HttpStatusCode.BadRequest };
                default:
                    return new ObjectResult(executionResult.GetExecutionErrors().First().Value) { StatusCode = (int)HttpStatusCode.InternalServerError };
            }
        }

        public static async Task<IActionResult> ToHttpResponseAsync(this Task<ExecutionResult> executionResultAsync)
        {
            var executionResult = await executionResultAsync;

            return ToHttpResponse(executionResult);
        }
    }
}