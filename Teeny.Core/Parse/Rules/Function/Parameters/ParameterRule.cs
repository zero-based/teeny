using Teeny.Core.Parse.Rules.Common;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Function.Parameters
{
    public class ParameterRule : BaseRule
    {
        public ParameterRule(TokenRecord dataType, TokenRecord identifier)
        {
            DataType.Assign(dataType);
            Identifier.Assign(identifier);
        }

        public ParameterRule()
        {
        }

        public TerminalNode DataType { get; set; } = new TerminalNode(Token.Int, Token.Float, Token.String);
        public TerminalNode Identifier { get; set; } = new TerminalNode(Token.Identifier);
    }
}