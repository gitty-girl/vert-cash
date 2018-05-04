using System.Collections.Generic;

namespace CurrencyConverter.ExecutionControl
{

    public class ExecutionResult<T> : ExecutionResult
    {
        public T Result { get; private set; }

        public ExecutionResult(T result)
        {
            Result = result;
        }

        public ExecutionResult(ExecutionResult executionResult, T result = default(T))
        {
            Result = result;
            ExecutionResultType = executionResult.ExecutionResultType;
            AddErrors(executionResult.GetErrorList());
        }

        public ExecutionResult()
        {
        }

        public static ExecutionResult<T> Success(T result) =>
            new ExecutionResult<T>(result);

        public void SetSuccessfulResult(T result)
        {
            Result = result;
            SetSuccessfulResult();
        }

        public new static ExecutionResult<T> SystemFailedResult()
        {
            var result = new ExecutionResult<T>();
            result.AddSystemError();

            return result;
        }

        public new static ExecutionResult<T> DomainFailedResult(Dictionary<string, string> errorMessages)
        {
            var result = new ExecutionResult<T>();
            result.AddErrors(errorMessages);

            return result;
        }
    }
}