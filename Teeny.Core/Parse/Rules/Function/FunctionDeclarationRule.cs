using Teeny.Core.Parse.Rules.Function.Parameters;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Function
{
    public class FunctionDeclarationRule : BaseRule
    {
        public FunctionDeclarationRule(TokenRecord dataType, TokenRecord functionName,
            TokenRecord leftParenthesisRecord,
            ParametersRule parameters, TokenRecord rightParenthesisRecord)
        {
            DataType.Assign(dataType);
            FunctionName.Assign(functionName);
            LeftParenthesis.Assign(leftParenthesisRecord);
            Parameters = parameters;
            RightParenthesis.Assign(rightParenthesisRecord);
        }

        public FunctionDeclarationRule()
        {
        }

        public TerminalNode DataType { get; set; } = new TerminalNode(Token.Int, Token.Float, Token.String);
        public TerminalNode FunctionName { get; set; } = new TerminalNode(Token.Identifier);
        public TerminalNode LeftParenthesis { get; set; } = new TerminalNode(Token.ParenthesisLeft);
        public ParametersRule Parameters { get; set; }
        public TerminalNode RightParenthesis { get; set; } = new TerminalNode(Token.ParenthesisRight);
    }
}