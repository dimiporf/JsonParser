using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonParser
{
    public static class JsonValidator
    {
        // Method to validate JSON value
        public static bool IsValidValue(string value)
        {
            // Trim whitespace characters from the value
            value = value.Trim();

            // Check if the value is a string (enclosed in double quotes)
            if (value.StartsWith("\"") && value.EndsWith("\""))
            {
                return true; // Valid string value
            }

            // Check if the value is a boolean (true or false)
            if (value == "true" || value == "false")
            {
                return true; // Valid boolean value
            }

            // Check if the value is null
            if (value == "null")
            {
                return true; // Valid null value
            }

            // Check if the value is numeric
            if (double.TryParse(value, out _))
            {
                return true; // Valid numeric value
            }

            // Check if the value is an object (starts with '{' and ends with '}')
            if (value.StartsWith("{") && value.EndsWith("}"))
            {
                // Validate nested object using JsonParser
                return JsonParser.IsValidJson(value, out _);
            }

            // Check if the value is an array (starts with '[' and ends with ']')
            if (value.StartsWith("[") && value.EndsWith("]"))
            {
                // Validate array
                return IsValidArray(value);
            }

            return false; // Invalid value format
        }

        // Method to validate JSON array
        private static bool IsValidArray(string array)
        {
            // Trim whitespace characters from the array
            array = array.Trim();

            // Check if the array starts with '[' and ends with ']'
            if (array.Length < 2 || array[0] != '[' || array[^1] != ']')
            {
                return false; // Not a valid array
            }

            // Remove outer brackets to isolate the array content
            string arrayContent = array.Substring(1, array.Length - 2).Trim();

            // Handle empty array
            if (string.IsNullOrWhiteSpace(arrayContent))
            {
                return true; // Valid empty array
            }

            // Split the array content into values
            var values = SplitArrayValues(arrayContent);

            // Validate each value in the array
            foreach (var value in values)
            {
                if (!IsValidValue(value))
                {
                    return false; // Invalid value in array
                }
            }

            return true; // Array is valid
        }

        // Method to split array values correctly, considering nested structures
        private static List<string> SplitArrayValues(string arrayContent)
        {
            var values = new List<string>();
            int braceDepth = 0;
            int bracketDepth = 0;
            int startIndex = 0;

            for (int i = 0; i < arrayContent.Length; i++)
            {
                char c = arrayContent[i];

                // Handle nested objects and arrays
                if (c == '{')
                {
                    braceDepth++;
                }
                else if (c == '}')
                {
                    braceDepth--;
                }
                else if (c == '[')
                {
                    bracketDepth++;
                }
                else if (c == ']')
                {
                    bracketDepth--;
                }
                else if (c == ',' && braceDepth == 0 && bracketDepth == 0)
                {
                    // Add the value to the list
                    values.Add(arrayContent.Substring(startIndex, i - startIndex).Trim());
                    startIndex = i + 1;
                }
            }

            // Add the last value to the list
            values.Add(arrayContent.Substring(startIndex).Trim());

            return values;
        }
    }
}