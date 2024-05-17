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

                    // Handle empty JSON object
                    if (string.IsNullOrWhiteSpace(objectContent))
                    {
                        return true;
                    }

                    // Parse key-value pairs
                    var keyValuePairs = SplitKeyValuePairs(objectContent);

                    foreach (var pair in keyValuePairs)
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

        // Method to split key-value pairs correctly, considering nested structures
        private static List<string> SplitKeyValuePairs(string objectContent)
        {
            var keyValuePairs = new List<string>();
            int braceDepth = 0;
            int bracketDepth = 0;
            int startIndex = 0;

            for (int i = 0; i < objectContent.Length; i++)
            {
                char c = objectContent[i];

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
                    keyValuePairs.Add(objectContent.Substring(startIndex, i - startIndex).Trim());
                    startIndex = i + 1;
                }
            }

            keyValuePairs.Add(objectContent.Substring(startIndex).Trim());

            return keyValuePairs;
        }
    }
}