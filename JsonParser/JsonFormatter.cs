using System.Collections.Generic;

namespace JsonParser
{
    /// <summary>
    /// The JsonFormatter class provides methods to format parsed JSON content into a structured log.
    /// </summary>
    public static class JsonFormatter
    {
        /// <summary>
        /// Formats the parsed JSON object contents into a structured log.
        /// </summary>
        /// <param name="parsedContents">The dictionary containing the parsed JSON key-value pairs.</param>
        /// <returns>A formatted string representing the structured log of the JSON object content.</returns>
        public static string FormatLog(Dictionary<string, string> parsedContents)
        {
            string log = "{\n";

            foreach (var pair in parsedContents)
            {
                log += $"  \"{pair.Key}\": \"{pair.Value}\",\n";
            }

            log = log.TrimEnd(',', '\n') + "\n}";
            return log;
        }

        /// <summary>
        /// Formats the parsed JSON array contents into a structured log.
        /// </summary>
        /// <param name="values">The list of values in the JSON array.</param>
        /// <returns>A formatted string representing the structured log of the JSON array content.</returns>
        public static string FormatArrayLog(List<string> values)
        {
            string log = "[\n";

            foreach (var value in values)
            {
                log += $"  \"{value}\",\n";
            }

            log = log.TrimEnd(',', '\n') + "\n]";
            return log;
        }
    }
}
