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

                        // Validate key format (must be enclosed in double quotes)
                        if (!(key.StartsWith("\"") && key.EndsWith("\"")))
                        {
                            // Key is not enclosed in double quotes
                            return false;
                        }

                        // Remove enclosing double quotes from key
                        key = key.Substring(1, key.Length - 2);

                        // Validate value format
                        if (!IsValidValue(value))
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

        // Method to validate JSON value
        static bool IsValidValue(string value)
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
