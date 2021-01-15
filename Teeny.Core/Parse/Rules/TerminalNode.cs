using System.Collections.Generic;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules
{
    public class TerminalNode : Node
    {
        public TerminalNode(TokenRecord tokenRecord)
        {
            Lexeme = tokenRecord.Lexeme;
            Token = tokenRecord.Token;
        }

        public TerminalNode()
        {
        }

        public string Lexeme { get; set; }
        public Token Token { get; set; }

        public override string Name => $"{Lexeme} ({Token})";
        public override IEnumerable<Node> Children => null;
    }
}