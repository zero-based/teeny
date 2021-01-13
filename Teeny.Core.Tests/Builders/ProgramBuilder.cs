using System.Collections.Generic;
using Teeny.Core.Parse.Rules;
using Teeny.Core.Parse.Rules.Function;

namespace Teeny.Core.Tests.Builders
{
    public class ProgramBuilder
    {
        private readonly ProgramRule _programRule;
        public ProgramRule Build => _programRule;

        public ProgramBuilder()
        {
            _programRule = new ProgramRule();
        }


        public ProgramBuilder WithMain(MainFunctionRule mainFunctionRule)
        {
            _programRule.MainFunction = mainFunctionRule;
            return this;
        }

        public ProgramBuilder WithFunctionStatement(ICollection<FunctionStatementRule> functionStatements)
        {
            _programRule.FunctionStatements = functionStatements;
            return this;
        }
    }
}