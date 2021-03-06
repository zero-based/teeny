﻿using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Rules.Equation
{
    public class ExtraEquationRule : BaseRule
    {
        public ExtraEquationRule(TerminalNode arithmeticOperator, EquationRule equation,
            ExtraEquationRule extraEquation)
        {
            ArithmeticOperator = Guard.OneOf(() => arithmeticOperator, TokensGroups.ArithmeticOperators);
            Equation = Guard.NonNull(() => equation);
            ExtraEquation = extraEquation;
        }

        public ExtraEquationRule()
        {
        }

        public TerminalNode ArithmeticOperator { get; set; }
        public EquationRule Equation { get; set; }
        public ExtraEquationRule ExtraEquation { get; set; }
    }
}