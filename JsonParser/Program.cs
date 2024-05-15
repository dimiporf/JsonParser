using System;
using System.IO;

namespace JsonParser
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exitRequested = false;

            while (!exitRequested)
            {
                // Prompt user to input file path
                Console.WriteLine("Enter the path to the file containing JSON (or type 'exit' to quit):");
                string input = Console.ReadLine();

                if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    exitRequested = true;
                    continue;
                }

                try
                {
                    // Read content of the file
                    string jsonContent = File.ReadAllText(input);

                    // Validate JSON content
                    bool isValidJson = IsValidJson(jsonContent);

                    // Display result
                    if (isValidJson)
                    {
                        Console.WriteLine("The JSON in the file is valid.");
                    }
                    else
                    {
                        Console.WriteLine("The JSON in the file is invalid.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        // Method to validate JSON content
        static bool IsValidJson(string json)
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

                        // Validate key and value format (must be enclosed in double quotes)
                        if (!(key.StartsWith("\"") && key.EndsWith("\"")) ||
                            !(value.StartsWith("\"") && value.EndsWith("\"")))
                        {
                            // Key or value is not enclosed in double quotes
                            return false;
                        }

                        // Remove enclosing double quotes from key and value
                        key = key.Substring(1, key.Length - 2);
                        value = value.Substring(1, value.Length - 2);

                        // Validate key and value (should not be empty)
                        if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
                        {
                            // Empty key or value
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

        // Method to parse a JSON object (basic implementation)
        static void ParseJsonObject(string json)
        {
            int depth = 0;

            // Iterate through each character in the JSON string
            for (int i = 0; i < json.Length; i++)
            {
                char c = json[i];

                if (c == '{')
                {
                    depth++;
                }
                else if (c == '}')
                {
                    depth--;

                    // If depth becomes negative, it means invalid nesting
                    if (depth < 0)
                    {
                        throw new Exception("Invalid JSON object");
                    }
                }
            }

            // After iterating through all characters, depth should be zero for valid JSON
            if (depth != 0)
            {
                throw new Exception("Invalid JSON object");
            }
        }
    }
}
