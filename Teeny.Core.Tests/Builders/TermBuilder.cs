using Teeny.Core.Parse.Rules.Common;
using Teeny.Core.Parse.Rules.Function;
using Teeny.Core.Scan;

namespace Teeny.Core.Tests.Builders
{
    public class TermBuilder
    {
        public TermBuilder()
        {
            Build = new TermRule();
        }

        public TermRule Build { get; }

        public TermBuilder AsNumber(string number)
        {
            Build.Number = new NumberBuilder(number).Build;
            return this;
        }

        public TermBuilder AsIdentifier(string identifier)
        {
            Build.Identifier = TerminalNodeBuilder.Of(Token.Identifier, identifier);
            return this;
        }

        public TermBuilder AsFunctionCall(FunctionCallRule functionCallRule)
        {
            Build.FunctionCall = functionCallRule;
            return this;
        }
    }
}