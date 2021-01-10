using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Common
{
    public class SignRule : BaseRule
    {
        public SignRule(TokenRecord sign)
        {
            Sign.Assign(sign);
        }

        public TerminalNode Sign { get; set; } = new TerminalNode(Token.Plus, Token.Minus);
    }
}