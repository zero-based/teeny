using System;

namespace Teeny.Core.Exceptions
{
    public class UnknownLexemeException : Exception
    {
        public UnknownLexemeException(string lexeme)
            : base($"Unknown Lexeme '{lexeme}'")
        {
        }
    }
}
