using Teeny.Core.Scan.Attributes;

namespace Teeny.Core.Scan
{
    public enum Token
    {
        [PatternToken(@"^[a-zA-Z]([a-zA-Z0-9])*$")] Identifier,
        [PatternToken(@"^(\+|-)?[0-9]+(\.[0-9]+)?$")] ConstantNumber,
        [PatternToken(@"^"".*""$")] ConstantString,

        [ConstantToken("int")] DataTypeInt,
        [ConstantToken("float")] DataTypeFloat,
        [ConstantToken("string")] DataTypeString,

        [ConstantToken("read")] StatementRead,
        [ConstantToken("write")] StatementWrite,

        [ConstantToken("repeat")] StatementRepeat,
        [ConstantToken("until")] StatementUntil,

        [ConstantToken("if")] StatementIf,
        [ConstantToken("elseif")] StatementElseIf,
        [ConstantToken("else")] StatementElse,
        [ConstantToken("then")] StatementThen,
        [ConstantToken("end")] StatementEnd,

        [ConstantToken("return")] StatementReturn,
        [ConstantToken("endl")] ConstantEndl,

        [ConstantToken(";")] SymbolSemicolon,
        [ConstantToken(",")] SymbolComma,

        [ConstantToken("+")] OperatorArithmeticPlus,
        [ConstantToken("-")] OperatorArithmeticMinus,
        [ConstantToken("*")] OperatorArithmeticMultiply,
        [ConstantToken("/")] OperatorArithmeticDivide,

        [ConstantToken(">")] OperatorConditionalGreaterThan,
        [ConstantToken("<")] OperatorConditionalLessThan,
        [ConstantToken("=")] OperatorConditionalEqual,
        [ConstantToken("<>")] OperatorConditionalNotEqual,

        [ConstantToken("&&")] OperatorBooleanAnd,
        [ConstantToken("||")] OperatorBooleanOr,

        [ConstantToken(":=")] OperatorAssignment,

        [ConstantToken("{")] CurlyBracketLeft,
        [ConstantToken("}")] CurlyBracketRight,

        [ConstantToken("(")] ParenthesisLeft,
        [ConstantToken(")")] ParenthesisRight
    }
}
