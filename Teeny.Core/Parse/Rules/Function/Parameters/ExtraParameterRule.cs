using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Function.Parameters
{
    public class ExtraParameterRule : BaseRule
    {
        public ExtraParameterRule(TerminalNode comma, ParameterRule parameter)
        {
            Comma = Guard.OneOf(() => comma, Token.Comma);
            Parameter = Guard.NonNull(() => parameter);
        }

        public ExtraParameterRule()
        {
        }

        public TerminalNode Comma { get; set; }
        public ParameterRule Parameter { get; set; }
    }
}