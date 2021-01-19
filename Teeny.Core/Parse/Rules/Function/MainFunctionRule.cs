using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Function
{
    public class MainFunctionRule : BaseRule
    {
        public MainFunctionRule(TerminalNode dataType, TerminalNode main,
            TerminalNode leftParenthesis, TerminalNode rightParenthesis,
            FunctionBodyRule functionBody)
        {
            DataType = Guard.OneOf(() => dataType, TokensGroups.DataTypes);
            Main = Guard.OneOf(() => main, Token.Main);
            LeftParenthesis = Guard.OneOf(() => leftParenthesis, Token.ParenthesisLeft);
            RightParenthesis = Guard.OneOf(() => rightParenthesis, Token.ParenthesisRight);
            FunctionBody = Guard.NonNull(() => functionBody);
        }

        public MainFunctionRule()
        {
        }

        public TerminalNode DataType { get; set; }
        public TerminalNode Main { get; set; }
        public TerminalNode LeftParenthesis { get; set; }
        public TerminalNode RightParenthesis { get; set; }
        public FunctionBodyRule FunctionBody { get; set; }
    }
}