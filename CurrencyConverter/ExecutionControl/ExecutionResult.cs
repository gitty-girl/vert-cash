using System.Collections.Generic;
using System.Linq;

namespace CurrencyConverter.ExecutionControl
{
    public class ExecutionResult
    {
        public ExecutionResultType ExecutionResultType { get; protected set; } = ExecutionResultType.Success;

        private readonly Dictionary<string, string> _errors = new Dictionary<string, string>();

        public void AddDomainError(string key, string message)
        {
            AddError(key, message);

            ExecutionResultType = ExecutionResultType.DomainError;
        }

        public void AddErrors(Dictionary<string, string> errorMessages)
        {
            foreach (var errorMessage in errorMessages)
                AddDomainError(errorMessage.Key, errorMessage.Value);
        }

        public void AddSystemError()
        {
            ClearErrors();
            AddError("SystemError", "System error occured");

            ExecutionResultType = ExecutionResultType.SystemError;
        }
        
        public Dictionary<string, string> GetErrorList()
        {
            return _errors;
        }

        public List<ExecutionError> GetExecutionErrors()
        {
            return _errors.Select(ExecutionError.FromKeyValuePair).ToList();
        }

        public void SetSuccessfulResult()
        {
            ClearErrors();
            ExecutionResultType = ExecutionResultType.Success;
        }

        protected void ClearErrors()
        {
            _errors.Clear();

            ExecutionResultType = ExecutionResultType.Success;
        }

        private void AddError(string key, string message)
        {
            _errors.Add(key, message);
        }

        public static ExecutionResult Success()
        {
            return new ExecutionResult();
        }

        public static ExecutionResult DomainFailedResult(Dictionary<string, string> errorMessages)
        {
            var result = new ExecutionResult();
            result.AddErrors(errorMessages);

            return result;
        }

        public static ExecutionResult SystemFailedResult()
        {
            var result = new ExecutionResult();
            result.AddSystemError();

            return result;
        }

        public bool IsValid()
        {
            return ExecutionResultType == ExecutionResultType.Success;
        }
    }
}