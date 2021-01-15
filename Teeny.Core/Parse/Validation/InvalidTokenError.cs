using System.Collections.Generic;
using Teeny.Core.Parse.Rules;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Validation
{
    internal class InvalidTokenError : IGuardError
    {
        public TerminalNode FieldValue { get; set; }
        public IEnumerable<Token> ExpectedTokens { get; set; }
        public string FieldName { get; set; }
    }
}