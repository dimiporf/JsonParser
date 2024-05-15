using JsonLexerFactory;
using JsonParserFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonParser.JsonParserFactory
{
    // The parser class that validates the JSON string using tokens from the lexer
    public class JsonParser
    {
        private readonly JsonLexer _lexer;  // The lexer instance
        private JsonToken _currentToken;    // The current token being processed

        // Constructor to initialize the parser with the lexer
        public JsonParser(JsonLexer lexer)
        {
            _lexer = lexer;
            _currentToken = _lexer.GetNextToken(); // Get the first token
        }

        // Method to parse the JSON and return true if valid, false if invalid
        public bool Parse()
        {
            // Check if the first token is a left brace
            if (_currentToken.Type == JsonTokenType.LeftBrace)
            {
                _currentToken = _lexer.GetNextToken(); // Get the next token
                                                       // Check if the next token is a right brace
                if (_currentToken.Type == JsonTokenType.RightBrace)
                {
                    _currentToken = _lexer.GetNextToken(); // Get the next token
                                                           // Check if the token after the right brace is EndOfFile
                    if (_currentToken.Type == JsonTokenType.EndOfFile)
                    {
                        return true; // Valid JSON
                    }
                }
            }
            return false; // Invalid JSON
        }
    }

}
