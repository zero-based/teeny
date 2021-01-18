using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Common
{
    public class SignRule : BaseRule
    {
        public SignRule(TerminalNode sign)
        {
            Sign = Guard.OneOf(() => sign, TokensGroups.Signs);
        }

        public SignRule()
        {
        }

        public TerminalNode Sign { get; set; }
    }
}