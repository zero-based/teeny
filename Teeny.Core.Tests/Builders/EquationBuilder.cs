using Teeny.Core.Parse.Rules.Equation;
using Teeny.Core.Parse.Rules.Function;

namespace Teeny.Core.Tests.Builders
{
    public class EquationBuilder
    {
        private readonly EquationRule _equationRule;

        public EquationBuilder()
        {
            _equationRule = new EquationRule();
        }

        public EquationRule Build => _equationRule;

        public EquationBuilder AsNumberTermEquation(string number)
        {
            _equationRule.Term = new TermBuilder().AsNumber(number).Build;
            return this;
        }

        public EquationBuilder AsIdentifierTermEquation(string identifier)
        {
            _equationRule.Term = new TermBuilder().AsIdentifier(identifier).Build;
            return this;
        }

        public EquationBuilder AsFunctionCallTermEquation(FunctionCallRule functionCallRule)
        {
            _equationRule.Term = new TermBuilder().AsFunctionCall(functionCallRule).Build;
            return this;
        }
    }
}
