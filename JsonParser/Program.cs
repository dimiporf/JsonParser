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

                    // Validate JSON content and get the structured log
                    if (JsonParser.IsValidJson(jsonContent, out string structuredLog))
                    {
                        // Display success message and structured log
                        Console.WriteLine("The JSON in the provided file is valid. Here are the contents:");
                        Console.WriteLine(structuredLog);
                    }
                    else
                    {
                        // Display invalid JSON message
                        Console.WriteLine("The JSON in the file is invalid.");
                    }
                }
                catch (Exception ex)
                {
                    // Display error message
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }

            // Wait for user input before exiting
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}