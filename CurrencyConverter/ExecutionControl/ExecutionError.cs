using System.Collections.Generic;

namespace CurrencyConverter.ExecutionControl
{
    public class ExecutionError
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public ExecutionError(string key, string value)
        {
            Key = key;
            Value = value;
        }


        public static ExecutionError FromKeyValuePair(KeyValuePair<string, string> kvPair)
        {
            return new ExecutionError(kvPair.Key, kvPair.Value);
        }

        public static KeyValuePair<string, string> ToKeyValuePair(ExecutionError executionError)
        {
            return new KeyValuePair<string, string>(executionError.Key, executionError.Value);
        }
    }
}