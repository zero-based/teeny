using System;
using System.Collections.Generic;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules
{
    public class TerminalNode : Node
    {
        private readonly ICollection<Token> _validTokens;

        public TerminalNode(params Token[] validTokens)
        {
            _validTokens = validTokens;
        }

        public TokenRecord TokenRecord { get; set; }

        public override string Name => TokenRecord == null ? "" : $"{TokenRecord.Lexeme} ({TokenRecord.Token})";

        public override IEnumerable<Node> Children => null;

        public void Assign(TokenRecord tokenRecord)
        {
            if (tokenRecord == null || !_validTokens.Contains(tokenRecord.Token))
                throw new ArgumentException();
            TokenRecord = tokenRecord;
        }
    }
}