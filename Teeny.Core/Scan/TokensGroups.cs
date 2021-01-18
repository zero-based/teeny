namespace Teeny.Core.Scan
{
    internal static class TokensGroups
    {
        public static TokensGroup Arguments { get; } = new TokensGroup { Token.Identifier, Token.ConstantString, Token.ConstantNumber };
        public static TokensGroup DataTypes { get; } = new TokensGroup { Token.Int, Token.Float, Token.String };
        public static TokensGroup Signs { get; } = new TokensGroup { Token.Plus, Token.Minus };
        public static TokensGroup ArithmeticOperators { get; } = new TokensGroup { Token.Plus, Token.Minus, Token.Multiply, Token.Divide };
        public static TokensGroup BooleanOperators { get; } = new TokensGroup { Token.And, Token.Or };
        public static TokensGroup ConditionOperators { get; } = new TokensGroup { Token.LessThan, Token.GreaterThan, Token.Equal, Token.NotEqual };
    }
}
