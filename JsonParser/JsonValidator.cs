using System.Collections.Generic;

namespace JsonParser
{
    /// <summary>
    /// The JsonValidator class provides methods to validate individual JSON values.
    /// </summary>
    public static class JsonValidator
    {
        /// <summary>
        /// Validates a JSON value.
        /// </summary>
        /// <param name="value">The JSON value to validate.</param>
        /// <returns>True if the value is valid, false otherwise.</returns>
        public static bool IsValidValue(string value)
        {
            // Trim whitespace characters from the value
            value = value.Trim();

            // Check if the value is a string (enclosed in double quotes)
            if (value.StartsWith("\"") && value.EndsWith("\"")) return true;

            // Check if the value is a boolean (true or false)
            if (value == "true" || value == "false") return true;

            // Check if the value is null
            if (value == "null") return true;

            // Check if the value is numeric
            if (double.TryParse(value, out _)) return true;

            // Check if the value is an object (starts with '{' and ends with '}')
            if (value.StartsWith("{") && value.EndsWith("}"))
            {
                // Validate the nested object using JsonParser
                return JsonParser.IsValidJson(value, out _);
            }

            // Check if the value is an array (starts with '[' and ends with ']')
            if (value.StartsWith("[") && value.EndsWith("]"))
            {
                // Validate the array
                return IsValidArray(value);
            }

            return false; // Invalid value format
        }

        /// <summary>
        /// Validates a JSON array.
        /// </summary>
        /// <param name="array">The JSON array to validate.</param>
        /// <returns>True if the array is valid, false otherwise.</returns>
        private static bool IsValidArray(string array)
        {
            // Trim whitespace characters from the array
            array = array.Trim();

            // Check if the array starts with '[' and ends with ']'
            if (array.Length < 2 || array[0] != '[' || array[^1] != ']') return false;

            // Remove outer brackets to isolate the array content
            string arrayContent = array.Substring(1, array.Length - 2).Trim();

            // Handle empty array
            if (string.IsNullOrWhiteSpace(arrayContent)) return true;

            // Split the array content into individual values
            var values = JsonSplitter.SplitArrayValues(arrayContent);

            // Validate each value in the array
            foreach (var value in values)
            {
                if (!IsValidValue(value)) return false;
            }

            return true; // Array is valid
        }
    }
}
