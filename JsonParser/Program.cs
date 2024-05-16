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
                    bool isValidJson = JsonParser.IsValidJson(jsonContent);

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
    }
}
