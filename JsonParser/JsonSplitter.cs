using System.Collections.Generic;

namespace JsonParser
{
    /// <summary>
    /// The JsonSplitter class provides methods to split JSON strings into key-value pairs or array values.
    /// </summary>
    public static class JsonSplitter
    {
        /// <summary>
        /// Splits a JSON object content into individual key-value pairs, considering nested structures.
        /// </summary>
        /// <param name="objectContent">The JSON object content to split.</param>
        /// <returns>A list of key-value pairs as strings.</returns>
        public static List<string> SplitKeyValuePairs(string objectContent)
        {
            var keyValuePairs = new List<string>();
            int braceDepth = 0;
            int startIndex = 0;
            bool insideString = false;

            for (int i = 0; i < objectContent.Length; i++)
            {
                char c = objectContent[i];

                if (c == '\"' && (i == 0 || objectContent[i - 1] != '\\'))
                {
                    insideString = !insideString;
                }
                else if (c == '{' && !insideString)
                {
                    braceDepth++;
                }
                else if (c == '}' && !insideString)
                {
                    braceDepth--;
                }
                else if (c == ',' && braceDepth == 0 && !insideString)
                {
                    keyValuePairs.Add(objectContent.Substring(startIndex, i - startIndex).Trim());
                    startIndex = i + 1;
                }
            }

            keyValuePairs.Add(objectContent.Substring(startIndex).Trim());
            return keyValuePairs;
        }

        /// <summary>
        /// Splits a JSON array content into individual values, considering nested structures.
        /// </summary>
        /// <param name="arrayContent">The JSON array content to split.</param>
        /// <returns>A list of values as strings.</returns>
        public static List<string> SplitArrayValues(string arrayContent)
        {
            var values = new List<string>();
            int braceDepth = 0;
            int startIndex = 0;
            bool insideString = false;

            for (int i = 0; i < arrayContent.Length; i++)
            {
                char c = arrayContent[i];

                if (c == '\"' && (i == 0 || arrayContent[i - 1] != '\\'))
                {
                    insideString = !insideString;
                }
                else if (c == '{' && !insideString)
                {
                    braceDepth++;
                }
                else if (c == '}' && !insideString)
                {
                    braceDepth--;
                }
                else if (c == ',' && braceDepth == 0 && !insideString)
                {
                    values.Add(arrayContent.Substring(startIndex, i - startIndex).Trim());
                    startIndex = i + 1;
                }
            }

            values.Add(arrayContent.Substring(startIndex).Trim());
            return values;
        }
    }
}
