using System;
using System.IO;

namespace JsonParser
{
    class Program
    {
        static void Main(string[] args)
        {
            // Main loop to handle user input for JSON file paths
            while (true)
            {
                Console.Write("Enter the path to the JSON file or 'exit' to quit: ");
                string input = Console.ReadLine();

                // Exit the loop if the user types 'exit'
                if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                // Read the JSON content from the provided file path
                if (File.Exists(input))
                {
                    string jsonContent = File.ReadAllText(input);
                    if (JsonParser.IsValidJson(jsonContent, out string structuredLog))
                    {
                        Console.WriteLine("The JSON in the provided file is valid. Here are the results:");
                        Console.WriteLine(structuredLog);
                    }
                    else
                    {
                        Console.WriteLine("Invalid JSON");
                    }
                }
                else
                {
                    Console.WriteLine("File not found. Please try again.");
                }

                // Prompt the user to press any key to exit or continue
                Console.WriteLine("Press any key to continue or type 'exit' to quit...");
                if (Console.ReadKey().KeyChar == 'e')
                {
                    break;
                }
            }
        }
    }
}
