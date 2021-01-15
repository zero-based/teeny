using System.Collections.Generic;
using Teeny.Core.Parse.Rules.Function;

namespace Teeny.Core.Parse.Rules
{
    public class ProgramRule : BaseRule
    {
        public ProgramRule(ICollection<FunctionStatementRule> functionStatements, MainFunctionRule mainFunction)
        {
            FunctionStatements = functionStatements;
            MainFunction = Guard.NonNull(() => mainFunction);
        }

        public ProgramRule()
        {
        }

        public ICollection<FunctionStatementRule> FunctionStatements { get; set; }
        public MainFunctionRule MainFunction { get; set; }
    }
}