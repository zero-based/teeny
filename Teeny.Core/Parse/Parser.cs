using System;
using System.Collections.Generic;
using Teeny.Core.Parse.Rules;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse
{
    public class Parser
    {
        public BaseRule Parse(List<TokenRecord> tokens)
        {
            return new ProgramRule();
        }
    }
}