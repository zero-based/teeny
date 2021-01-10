using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Common
{
    public class DataTypeRule : BaseRule
    {
        public DataTypeRule(TokenRecord dataType)
        {
            DataType.Assign(dataType);
        }

        public DataTypeRule()
        {
        }

        public TerminalNode DataType { get; set; } = new TerminalNode(Token.Int, Token.Float, Token.String);
    }
}