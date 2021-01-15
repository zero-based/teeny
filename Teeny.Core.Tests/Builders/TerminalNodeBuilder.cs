using Teeny.Core.Parse.Rules;
using Teeny.Core.Scan;
using Teeny.Core.Scan.Attributes;

namespace Teeny.Core.Tests.Builders
{
    public static class TerminalNodeBuilder
    {
        public static TerminalNode Of(Token token)
        {
            return new TerminalNode
            {
                Token = token,
                Lexeme = token.GetAttributeOfType<ConstantTokenAttribute>().ReservedWord
            };
        }

        public static TerminalNode Of(Token token, string lexeme)
        {
            return new TerminalNode
            {
                Token = token,
                Lexeme = lexeme
            };
        }
    }
}