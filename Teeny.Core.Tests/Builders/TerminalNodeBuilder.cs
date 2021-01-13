using Teeny.Core.Parse.Rules;
using Teeny.Core.Scan;
using Teeny.Core.Scan.Attributes;

namespace Teeny.Core.Tests.Builders
{
    public class TerminalNodeBuilder
    {
        public static TerminalNode Of(Token token)
        {
            return new TerminalNode
            {
                TokenRecord = new TokenRecord
                {
                    Token = token,
                    Lexeme = token.GetAttributeOfType<ConstantTokenAttribute>().ReservedWord
                }
            };
        }

        public static TerminalNode Of(Token token, string lexeme)
        {
            return new TerminalNode
            {
                TokenRecord = new TokenRecord
                {
                    Token = token,
                    Lexeme = lexeme
                }
            };
        }
    }
}