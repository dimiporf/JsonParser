using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace JsonParser
{
    /// <summary>
    /// The JsonParser class provides methods for validating and parsing JSON content.
    /// </summary>
    public static class JsonParser
    {
        /// <summary>
        /// Validates the provided JSON content and generates a structured log of the parsed contents.
        /// </summary>
        /// <param name="json">The JSON string to validate and parse.</param>
        /// <param name="structuredLog">The structured log of the parsed JSON content.</param>
        /// <returns>True if the JSON is valid, false otherwise.</returns>
        public static bool IsValidJson(string json, out string structuredLog)
        {
            structuredLog = "";

            if (string.IsNullOrWhiteSpace(json))
            {
                return false; // Empty JSON
            }

            // Remove line breaks and extra whitespace within the JSON string
            json = json.Replace("\n", "").Replace("\r", "").Replace("\t", "").Replace(" ", "");

            if (!IsJsonSyntaxValid(json))
            {
                return false; // Invalid JSON syntax
            }

            if (json.StartsWith("[") && json.EndsWith("]"))
            {
                return HandleArray(json, out structuredLog);
            }
            else if (json.StartsWith("{") && json.EndsWith("}"))
            {
                return HandleObject(json, out structuredLog);
            }

            return false; // JSON format is invalid
        }

        // Method to handle JSON arrays
        private static bool HandleArray(string json, out string structuredLog)
        {
            structuredLog = "";
            json = json.Substring(1, json.Length - 2).Trim();

            if (string.IsNullOrWhiteSpace(json))
            {
                structuredLog = "[]"; // Empty array
                return true;
            }

            var values = JsonSplitter.SplitArrayValues(json);

            structuredLog = JsonFormatter.FormatArrayLog(values);
            return true;
        }

        // Method to handle JSON objects
        private static bool HandleObject(string json, out string structuredLog)
        {
            structuredLog = "";
            json = json.Substring(1, json.Length - 2).Trim();

            if (string.IsNullOrWhiteSpace(json))
            {
                structuredLog = "{}"; // Empty object
                return true;
            }

            var keyValuePairs = JsonSplitter.SplitKeyValuePairs(json);
            var parsedContents = new Dictionary<string, string>();

            foreach (var pair in keyValuePairs)
            {
                int colonIndex = pair.IndexOf(':');
                if (colonIndex == -1) return false;

                string key = pair.Substring(0, colonIndex).Trim();
                string value = pair.Substring(colonIndex + 1).Trim();

                if (!(key.StartsWith("\"") && key.EndsWith("\""))) return false;
                key = key.Substring(1, key.Length - 2);

                if (!string.IsNullOrEmpty(key)) // Skip invalid or empty keys
                {
                    parsedContents[key] = value;
                }
            }

            structuredLog = JsonFormatter.FormatLog(parsedContents);
            return true;
        }

        // Method to check if JSON syntax is valid
        private static bool IsJsonSyntaxValid(string json)
        {
            try
            {
                // Attempt to parse the JSON string
                // If parsing succeeds, JSON syntax is valid
                JToken.Parse(json);
                return true;
            }
            catch (Exception)
            {
                // Parsing failed, JSON syntax is invalid
                return false;
            }
        }
    }
}
