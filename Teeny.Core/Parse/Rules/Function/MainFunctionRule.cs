using Teeny.Core.Parse.Rules.Common;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Function
{
    public class MainFunctionRule : BaseRule
    {
        public MainFunctionRule(DataTypeRule dataType, TokenRecord main,
            TokenRecord leftParenthesis, TokenRecord rightParenthesis,
            FunctionBodyRule functionBody)
        {
            DataType = dataType;
            Main.Assign(main);
            LeftParenthesis.Assign(leftParenthesis);
            RightParenthesis.Assign(rightParenthesis);
            FunctionBody = functionBody;
        }

        public MainFunctionRule()
        {
        }

        public DataTypeRule DataType { get; set; }
        public TerminalNode Main { get; set; } = new TerminalNode(Token.Main);
        public TerminalNode LeftParenthesis { get; set; } = new TerminalNode(Token.ParenthesisLeft);
        public TerminalNode RightParenthesis { get; set; } = new TerminalNode(Token.ParenthesisRight);
        public FunctionBodyRule FunctionBody { get; set; }
    }
}