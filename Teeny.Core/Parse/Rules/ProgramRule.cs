using System.Collections.Generic;
using Teeny.Core.Parse.Rules.Function;

namespace Teeny.Core.Parse.Rules
{
    public class ProgramRule : BaseRule
    {
        public ProgramRule(ICollection<FunctionStatementRule> functionStatement, MainFunctionRule mainFunction)
        {
            FunctionStatement = functionStatement;
            MainFunction = mainFunction;
        }

        public ProgramRule()
        {
        }

        public ICollection<FunctionStatementRule> FunctionStatement { get; set; }
        public MainFunctionRule MainFunction { get; set; }
    }
}