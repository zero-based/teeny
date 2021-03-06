using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Function.Parameters
{
    public class ParameterRule : BaseRule
    {
        public ParameterRule(TerminalNode dataType, TerminalNode identifier)
        {
            DataType = Guard.OneOf(() => dataType, TokensGroups.DataTypes);
            Identifier = Guard.OneOf(() => identifier, Token.Identifier);
        }

        public ParameterRule()
        {
        }

        public TerminalNode DataType { get; set; }
        public TerminalNode Identifier { get; set; }
    }
}