using Teeny.Core.Parse.Rules.Common;
using Teeny.Core.Parse.Rules.Function.Parameters;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Function
{
    public class FunctionDeclarationRule : BaseRule
    {
        public FunctionDeclarationRule(DataTypeRule dataType, FunctionNameRule functionName,
            TokenRecord leftParenthesisRecord,
            ParametersRule parameters, TokenRecord rightParenthesisRecord)
        {
            DataType = dataType;
            FunctionName = functionName;
            LeftParenthesis.Assign(leftParenthesisRecord);
            Parameters = parameters;
            RightParenthesis.Assign(rightParenthesisRecord);
        }

        public DataTypeRule DataType { get; set; }
        public FunctionNameRule FunctionName { get; set; }
        public TerminalNode LeftParenthesis { get; set; } = new TerminalNode(Token.ParenthesisLeft);
        public ParametersRule Parameters { get; set; }
        public TerminalNode RightParenthesis { get; set; } = new TerminalNode(Token.ParenthesisRight);
    }
}