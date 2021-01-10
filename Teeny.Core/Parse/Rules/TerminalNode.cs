using System;
using System.Collections.Generic;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules
{
    public class TerminalNode
    {
        private readonly ICollection<Token> _validTokens;

        public TerminalNode(params Token[] validTokens)
        {
            _validTokens = validTokens;
        }

        public TokenRecord TokenRecord { get; set; }

        public void Assign(TokenRecord tokenRecord)
        {
            if (!_validTokens.Contains(tokenRecord.Token))
                throw new ArgumentException();
            TokenRecord = tokenRecord;
        }
    }
}