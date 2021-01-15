using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Function.Arguments
{
    public class ArgumentsRule : BaseRule
    {
        public ArgumentsRule(TerminalNode argument, ExtraArgumentRule extraArgument)
        {
            Argument = Guard.OneOf(() => argument, Token.Identifier, Token.ConstantString, Token.ConstantNumber);
            ExtraArgument = extraArgument;
        }

        public ArgumentsRule()
        {
        }

        public TerminalNode Argument { get; set; }
        public ExtraArgumentRule ExtraArgument { get; set; }
    }
}