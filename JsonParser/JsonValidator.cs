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

            return false; // Invalid value format
        }
    }
}
