using Teeny.Core.Scan.Attributes;

namespace Teeny.Core.Scan
{
    public enum Token
    {
        [PatternToken(@"^[a-zA-Z]([a-zA-Z0-9])*$")] Identifier,

        [PatternToken(@"^(\+|-)?[0-9]+(\.[0-9]+)?$")] ConstantNumber,
        [PatternToken(@"^"".*""$")] ConstantString,

        [ConstantToken("int")] Int,
        [ConstantToken("float")] Float,
        [ConstantToken("string")] String,

        [ConstantToken("read")] Read,
        [ConstantToken("write")] Write,

        [ConstantToken("repeat")] Repeat,
        [ConstantToken("until")] Until,

        [ConstantToken("if")] If,
        [ConstantToken("elseif")] ElseIf,
        [ConstantToken("else")] Else,
        [ConstantToken("then")] Then,
        [ConstantToken("end")] End,

        [ConstantToken("return")] Return,
        [ConstantToken("endl")] Endl,
        [ConstantToken("main")] Main,

        [ConstantToken(";")] Semicolon,
        [ConstantToken(",")] Comma,

        [ConstantToken("+")] Plus,
        [ConstantToken("-")] Minus,
        [ConstantToken("*")] Multiply,
        [ConstantToken("/")] Divide,

        [ConstantToken(">")] GreaterThan,
        [ConstantToken("<")] LessThan,
        [ConstantToken("=")] Equal,
        [ConstantToken("<>")] NotEqual,

        [ConstantToken("&&")] And,
        [ConstantToken("||")] Or,

        [ConstantToken(":=")] Assignment,

        [ConstantToken("{")] CurlyBracketLeft,
        [ConstantToken("}")] CurlyBracketRight,

        [ConstantToken("(")] ParenthesisLeft,
        [ConstantToken(")")] ParenthesisRight,

        [IgnoredToken] [PatternToken(@"^/\*[\s\S]*\*/$")] Comment,
        [IgnoredToken] [PatternToken(@"^\s+$")] Whitespace,
        [IgnoredToken] Unknown
    }
}