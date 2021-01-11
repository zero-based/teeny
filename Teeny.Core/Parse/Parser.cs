using System;
using System.Collections.Generic;
using System.Linq;
using Teeny.Core.Parse.Rules;
using Teeny.Core.Parse.Rules.Common;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse
{
    public class Parser
    {
        private Queue<TokenRecord> _tokensQueue;
        private TokenRecord CurrentRecord => _tokensQueue.Peek();
        public List<string> ErrorList = new List<string>();

        private TokenRecord Match(params Token[] tokens)
        {
            if (tokens.Contains(CurrentRecord.Token))
            {
                var tokenRecord = CurrentRecord;
                _tokensQueue.Dequeue();
                return tokenRecord;
            }

            ErrorList.Add($"Expected ${string.Join("or", tokens)} , found {CurrentRecord.Lexeme}");
            return null;
        }

        private static T TryBuild<T>(Func<T> ruleBuilder)
        {
            try
            {
                return ruleBuilder();
            }
            catch (Exception)
            {
                return default;
            }
        }

        public BaseRule Parse(List<TokenRecord> tokens)
        {
            _tokensQueue = new Queue<TokenRecord>(tokens);
            try
            {
                return ParseProgram();
            }
            catch (InvalidOperationException e)
            {
                // End of code!
                Console.WriteLine(e);
                return null;
            }
        }

        private BaseRule ParseProgram()
        {
            throw new NotImplementedException();
        }

        private TermRule ParseTerm()
        {
            if (CurrentRecord.Token == Token.Plus ||
                CurrentRecord.Token == Token.Minus ||
                CurrentRecord.Token == Token.ConstantNumber)
            {
                var number = ParseNumber();
                return number != null ? new TermRule(number) : null;
            }

            var identifier = Match(Token.Identifier);
            return TryBuild(() => new TermRule(identifier));
        }

        private NumberRule ParseNumber()
        {
            if (CurrentRecord.Token == Token.Plus || CurrentRecord.Token == Token.Minus)
            {
                var sign = ParseSign();
                var number = Match(Token.ConstantNumber);
                return TryBuild(() => new NumberRule(sign, number));
            }

            var number2 = Match(Token.ConstantNumber);
            return TryBuild(() => new NumberRule(number2));
        }

        private SignRule ParseSign()
        {
            var sign = Match(Token.Plus, Token.Minus);
            return TryBuild(() => new SignRule(sign));
        }
    }
}
