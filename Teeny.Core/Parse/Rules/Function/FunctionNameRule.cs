using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Function
{
    public class FunctionNameRule : BaseRule
    {
        public FunctionNameRule(TokenRecord identifier)
        {
            Identifier.Assign(identifier);
        }

        public TerminalNode Identifier { get; set; } = new TerminalNode(Token.Identifier);
    }
}