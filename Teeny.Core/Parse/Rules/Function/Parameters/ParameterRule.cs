using Teeny.Core.Parse.Rules.Common;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Function.Parameters
{
    public class ParameterRule : BaseRule
    {
        public ParameterRule(DataTypeRule dataType, TokenRecord identifier)
        {
            DataType = dataType;
            Identifier.Assign(identifier);
        }

        public DataTypeRule DataType { get; set; }
        public TerminalNode Identifier { get; set; } = new TerminalNode(Token.Identifier);
    }
}