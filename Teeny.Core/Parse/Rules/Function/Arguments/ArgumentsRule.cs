using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Function.Arguments
{
    public class ArgumentsRule : BaseRule
    {
        public ArgumentsRule(TokenRecord argument, ExtraArgumentRule extraArgument)
        {
            Argument.Assign(argument);
            ExtraArgument = extraArgument;
        }

        public ArgumentsRule()
        {
        }

        public TerminalNode Argument { get; set; } = new TerminalNode(Token.Identifier, Token.ConstantString, Token.ConstantNumber);
        public ExtraArgumentRule ExtraArgument { get; set; }
    }
}