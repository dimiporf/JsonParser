using JsonParserFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonLexerFactory
{
    // The lexer class that tokenizes the input JSON string
    public class JsonLexer
    {
        private readonly string _json; // The input JSON string
        private int _position;         // Current position in the JSON string

        // Constructor to initialize the lexer with the JSON string
        public JsonLexer(string json)
        {
            _json = json;
            _position = 0;
        }

        // Method to get the next token from the JSON string
        public JsonToken GetNextToken()
        {
            // Skip white spaces
            while (_position < _json.Length && char.IsWhiteSpace(_json[_position]))
            {
                _position++;
            }

            // If reached the end of the string, return EndOfFile token
            if (_position >= _json.Length)
            {
                return new JsonToken(JsonTokenType.EndOfFile, null);
            }

            // Get the current character
            char current = _json[_position];

            // Determine the token type based on the current character
            switch (current)
            {
                case '{':
                    _position++;
                    return new JsonToken(JsonTokenType.LeftBrace, "{");
                case '}':
                    _position++;
                    return new JsonToken(JsonTokenType.RightBrace, "}");
                default:
                    return new JsonToken(JsonTokenType.Invalid, current.ToString());
            }
        }
    }

}
