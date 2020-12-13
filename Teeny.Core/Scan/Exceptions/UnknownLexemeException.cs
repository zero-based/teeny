using System;

namespace Teeny.Core.Scan.Exceptions
{
    public class UnknownLexemeException : Exception
    {
        public UnknownLexemeException(string lexeme)
            : base($"Unknown Lexeme '{lexeme}'")
        {
        }
    }
}
