using Teeny.Core.Parse.Rules.Function.Parameters;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Function
{
    public class FunctionDeclarationRule : BaseRule
    {
        public FunctionDeclarationRule(TerminalNode dataType, TerminalNode functionName,
            TerminalNode leftParenthesis, ParametersRule parameters,
            TerminalNode rightParenthesis)
        {
            DataType = Guard.OneOf(() => dataType, TokensGroups.DataTypes);
            FunctionName = Guard.OneOf(() => functionName, Token.Identifier);
            LeftParenthesis = Guard.OneOf(() => leftParenthesis, Token.ParenthesisLeft);
            Parameters = parameters;
            RightParenthesis = Guard.OneOf(() => rightParenthesis, Token.ParenthesisRight);
        }

        public FunctionDeclarationRule()
        {
        }

        public TerminalNode DataType { get; set; }
        public TerminalNode FunctionName { get; set; }
        public TerminalNode LeftParenthesis { get; set; }
        public ParametersRule Parameters { get; set; }
        public TerminalNode RightParenthesis { get; set; }
    }
}