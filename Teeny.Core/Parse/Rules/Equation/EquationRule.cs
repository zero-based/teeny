namespace Teeny.Core.Parse.Rules.Equation
{
    public class EquationRule : BaseRule
    {
        public EquationRule(AnyEquationRule anyEquation, ExtraEquationRule extraEquation)
        {
            AnyEquation = anyEquation;
            ExtraEquation = extraEquation;
        }

        public EquationRule()
        {
        }

        public AnyEquationRule AnyEquation { get; set; }

        public ExtraEquationRule ExtraEquation { get; set; }
    }
}