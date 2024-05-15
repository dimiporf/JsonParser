using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonParser.JsonToken
{
    // Define the possible types of tokens in JSON
    public enum JsonTokenType
    {
        LeftBrace,    // '{'
        RightBrace,   // '}'
        EndOfFile,    // End of the JSON input
        Invalid       // Invalid token
    }

    // Represents a token with a type and value
    public class JsonToken
    {
        public JsonTokenType Type { get; }  // The type of token
        public string Value { get; }        // The value of the token

        // Constructor to initialize the token with type and value
        public JsonToken(JsonTokenType type, string value)
        {
            Type = type;
            Value = value;
        }
    }

}
