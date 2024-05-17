# JsonParser

JsonParser is a C# library that provides methods for validating and parsing JSON content. This implementation follows specific guidelines and features custom classes for handling JSON validation, parsing, formatting, and splitting.

## Features

- Validates JSON syntax and structure
- Parses JSON objects and arrays into key-value pairs
- Generates structured logs of the parsed JSON content
- Handles nested structures in JSON objects and arrays

## Implementation Details

### JsonParser

The `JsonParser` class provides methods for validating JSON content and generating structured logs of the parsed contents. It includes the following key methods:

- `IsValidJson`: Validates the provided JSON content and generates a structured log of the parsed contents.
- `HandleArray`: Handles JSON arrays, parsing individual values and generating a structured log.
- `HandleObject`: Handles JSON objects, parsing key-value pairs and generating a structured log.
- `IsValidValue`: Validates individual JSON values, including strings, booleans, null, numbers, objects, and arrays.
- `UnescapeJsonString`: Unescapes JSON strings, removing escape characters.
- `SplitArrayValues`: Splits JSON array content into individual values, considering nested structures.
- `IsJsonSyntaxValid`: Checks if JSON syntax is valid using `JToken.Parse` from Newtonsoft.Json.

### JsonFormatter

The `JsonFormatter` class provides methods for formatting parsed JSON content into a structured log. It includes the following methods:

- `FormatLog`: Formats parsed JSON object contents into a structured log.
- `FormatArrayLog`: Formats parsed JSON array contents into a structured log.

### JsonSplitter

The `JsonSplitter` class provides methods for splitting JSON strings into key-value pairs or array values. It includes the following methods:

- `SplitKeyValuePairs`: Splits a JSON object content into individual key-value pairs, considering nested structures.
- `SplitArrayValues`: Splits a JSON array content into individual values, considering nested structures.

### JsonValidator

The `JsonValidator` class provides methods to validate individual JSON values. It includes the following method:

- `IsValidValue`: Validates a JSON value, including strings, booleans, null, numbers, objects, and arrays.

### Program.cs

The `Program.cs` file contains an example usage of the JsonParser library. It demonstrates how to validate and parse JSON content, and output the structured log of the parsed contents.

## Usage

### Installation

Clone this repository or download the source code directly.

### Features
- Validates JSON syntax and structure.
- Parses JSON objects and arrays.
- Generates a structured log of the parsed JSON content.

### To run the program, follow these steps:

Ensure you have the .NET Core SDK installed on your machine. You can download it from the official .NET website.

Clone this repository to your local machine:
- `git clone https://github.com/dimiporf/JsonParser.git`

Navigate to the directory containing the code files:
- `cd JsonParser`

Compile the C# code using the following command:
- `dotnet build`

If the build is successful, you can run the program using the following command:
- `dotnet run`

Follow the prompts to enter the path to the JSON file you want to parse. You can also type 'exit' to quit the program.

The program will validate the JSON content and display the structured log of the parsed JSON content in the console.

After reviewing the results, you can press any key to continue or type 'exit' to quit the program.
