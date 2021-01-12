using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Common
{
    public class NumberRule : BaseRule
    {
        public NumberRule(SignRule sign, TokenRecord number)
        {
            Sign = sign;
            Number.Assign(number);
        }

        public NumberRule(TokenRecord number)
        {
            Number.Assign(number);
        }

        public NumberRule()
        {
        }

        public SignRule Sign { get; set; }
        public TerminalNode Number { get; set; } = new TerminalNode(Token.ConstantNumber);
    }
}