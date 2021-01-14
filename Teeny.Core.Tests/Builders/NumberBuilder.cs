using Teeny.Core.Parse.Rules.Common;
using Teeny.Core.Scan;

namespace Teeny.Core.Tests.Builders
{
    public class NumberBuilder
    {
        private readonly NumberRule _numberRule;
        public NumberRule Build => _numberRule;

        public NumberBuilder(string number)
        {
            _numberRule = new NumberRule { Number = TerminalNodeBuilder.Of(Token.ConstantNumber, number) };
        }

        public NumberBuilder WithPluSign()
        {
            _numberRule.Sign = new SignRule { Sign = TerminalNodeBuilder.Of(Token.Plus) };
            return this;
        }

        public NumberBuilder WithMinusSign()
        {
            _numberRule.Sign = new SignRule { Sign = TerminalNodeBuilder.Of(Token.Minus) };
            return this;
        }
    }
}