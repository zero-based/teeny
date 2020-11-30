namespace Teeny.Core
{
    public enum Token
    {
        DataTypeInt,
        DataTypeFloat,
        DataTypeString,

        StatementRead,
        StatementWrite,

        StatementRepeat,
        StatementUntil,

        StatementIf,
        StatementElseIf,
        StatementElse,
        StatementThen,

        StatementReturn,
        ConstantEndl,

        SymbolSemicolon,
        SymbolComma,

        Identifier,

        ConstantString,
        ConstantNumber,

        OperatorArithmeticPlus,
        OperatorArithmeticMinus,
        OperatorArithmeticMultiply,
        OperatorArithmeticDivide,

        OperatorConditionalGreaterThan,
        OperatorConditionalLessThan,
        OperatorConditionalEqual,
        OperatorConditionalNotEqual,

        OperatorBooleanAnd,
        OperatorBooleanOr,

        OperatorAssignment,

        CurlyBracketLeft,
        CurlyBracketRight,

        ParenthesisLeft,
        ParenthesisRight
    }
}
