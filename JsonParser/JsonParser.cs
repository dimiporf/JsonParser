using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonParser
{
    public class JsonParser
    {
        // Method to validate JSON content
        public static bool IsValidJson(string json)
        {
            // Trim whitespace characters from the JSON content
            json = json.Trim();

            // Check if the JSON content starts with '{' and ends with '}'
            if (json.StartsWith("{") && json.EndsWith("}"))
            {
                try
                {
                    // Remove outer braces to isolate the object content
                    string objectContent = json.Substring(1, json.Length - 2).Trim();

                    // Split the object content into key-value pairs
                    string[] keyValuePairs = objectContent.Split(',');

                    foreach (string pair in keyValuePairs)
                    {
                        // Split each pair into key and value
                        int colonIndex = pair.IndexOf(':');
                        if (colonIndex == -1)
                        {
                            // Invalid key-value pair format (missing colon)
                            return false;
                        }

                        string key = pair.Substring(0, colonIndex).Trim();
                        string value = pair.Substring(colonIndex + 1).Trim();

                        // Validate key format (must be enclosed in double quotes)
                        if (!(key.StartsWith("\"") && key.EndsWith("\"")))
                        {
                            // Key is not enclosed in double quotes
                            return false;
                        }

                        // Remove enclosing double quotes from key
                        key = key.Substring(1, key.Length - 2);

                        // Validate value format
                        if (!JsonValidator.IsValidValue(value))
                        {
                            // Invalid value format
                            return false;
                        }

                        // Key should not be empty
                        if (string.IsNullOrEmpty(key))
                        {
                            // Empty key
                            return false;
                        }
                    }

                    return true; // JSON object is valid
                }
                catch (Exception)
                {
                    // Parsing or validation failed
                    return false;
                }
            }

            return false; // JSON format is invalid
        }
    }
}
