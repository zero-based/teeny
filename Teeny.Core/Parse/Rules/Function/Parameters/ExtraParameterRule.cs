using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Function.Parameters
{
    public class ExtraParameterRule : BaseRule
    {
        public ExtraParameterRule(TokenRecord comma, ParameterRule parameter)
        {
            Comma.Assign(comma);
            Parameter = parameter;
        }

        public ExtraParameterRule()
        {
        }

        public TerminalNode Comma { get; set; } = new TerminalNode(Token.Comma);
        public ParameterRule Parameter { get; set; }
    }
}