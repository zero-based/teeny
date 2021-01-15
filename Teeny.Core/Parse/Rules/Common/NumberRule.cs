using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Common
{
    public class NumberRule : BaseRule
    {
        public NumberRule(SignRule sign, TerminalNode number)
        {
            Sign = Guard.NonNull(() => sign);
            Number = Guard.OneOf(() => number, Token.ConstantNumber);
        }

        public NumberRule(TerminalNode number)
        {
            Number = Guard.OneOf(() => number, Token.ConstantNumber);
        }

        public NumberRule()
        {
        }

        public SignRule Sign { get; set; }
        public TerminalNode Number { get; set; }
    }
}