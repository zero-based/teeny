using System.ComponentModel.DataAnnotations;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Function.Arguments
{
    public class ArgumentsRule : BaseRule
    {
        public ArgumentsRule(TokenRecord identifier)
        {
            Identifier.Assign(identifier);
        }

        public ArgumentsRule(TokenRecord identifier, ExtraArgumentRule extraArgument)
        {
            Identifier.Assign(identifier);
            ExtraArgument = extraArgument;
        }

        public ArgumentsRule()
        {
        }

        public TerminalNode Identifier { get; set; } = new TerminalNode(Token.Identifier, Token.ConstantString, Token.ConstantNumber);
        public ExtraArgumentRule ExtraArgument { get; set; }
    }
}