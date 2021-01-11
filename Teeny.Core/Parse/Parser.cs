using System;
using System.Collections.Generic;
using System.Linq;
using Teeny.Core.Parse.Rules;
using Teeny.Core.Parse.Rules.Common;
using Teeny.Core.Parse.Rules.Function;
using Teeny.Core.Parse.Rules.Function.Arguments;
using Teeny.Core.Parse.Rules.Function.Parameters;
using Teeny.Core.Parse.Rules.Statements;
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

        private FunctionBodyRule ParseFunctionBody()
        {
            var leftCurlyBracket = Match(Token.CurlyBracketLeft);
            var statements = ParseStatements();
            var returnStatement = ParseReturnStatement();
            var rightCurlyBracket = Match(Token.ParenthesisRight);
            if (statements != null && returnStatement != null)
            {
                return TryBuild(() => new FunctionBodyRule(leftCurlyBracket, statements, returnStatement, rightCurlyBracket));
            }
            return null;
        }

        private FunctionNameRule ParseFunctionName()
        {
            var functionName = Match(Token.Identifier);
            return TryBuild(() => new FunctionNameRule(functionName));
        }

        private ExtraArgumentRule ParseExtraArgument()
        {
            var comma = Match(Token.Comma);
            var identifier = Match(Token.Identifier);
            return TryBuild(() => new ExtraArgumentRule(comma, identifier));
        }

        private ArgumentsRule ParseArgumentRule()
        {
            var identifier = Match(Token.Identifier);
            var extraArgument = ParseExtraArgument();
            if (extraArgument != null)
            {
                return TryBuild(() => new ArgumentsRule(identifier, extraArgument));
            }
            return null;
        }

        private FunctionStatementRule ParseFunctionStatement()
        {
            var functionBody = ParseFunctionBody();
            var functionDeclaration = ParseFunctionDeclaration();
            if (functionBody != null && functionDeclaration != null)
            {
                return TryBuild(() => new FunctionStatementRule(functionDeclaration, functionBody));
            }
            return null;
        }

        private FunctionCallRule ParseFunctionCall()
        {
            var identifier = Match(Token.Identifier);
            var parenthesisLeft = Match(Token.ParenthesisLeft);
            var arguments = ParseArgumentRule();
            var parenthesisRight = Match(Token.ParenthesisRight);
            if (arguments != null)
            {
                return TryBuild(() => new FunctionCallRule(identifier, parenthesisLeft, arguments, parenthesisRight));
            }
            return null;
        }
        private ReadStatementRule ParseRaedStatement()
        {
            var read = Match(Token.Read);
            var identifier = Match(Token.Identifier);
            var semicolon = Match(Token.Semicolon);
            return TryBuild(() => new ReadStatementRule(read, identifier, semicolon));
        }
        private ReturnStatementRule ParseReturnStatement()
        {
            var Return = Match(Token.Return);
            var expression = ParseExpression();
            var semicolon = Match(Token.Semicolon);
            return expression != null ?
             TryBuild(() => new ReturnStatementRule(Return, expression, semicolon)) : null;
        }
        private AssignmentStatementRule ParseAssignmentStatmenet()
        {

            var identifier = Match(Token.Identifier);
            var assignment = Match(Token.Assignment);
            var expression = ParseExpression();
            var semicolon = Match(Token.Semicolon);
            return expression != null ?
                TryBuild(() => new AssignmentStatementRule(identifier, assignment, expression, semicolon)) : null;

        }
        private ParameterRule ParseParameter()
        {
            var dataType = ParseDataType();
            var identifer = ParseExpression();
            return identifer != null ?
             TryBuild(() => new ParameterRule(dataType, identifer)) : null;
        }
        private ParametersRule ParseParameters()
        {
            var parameter = ParseParameter();
            var extraParameter = ParseExtraParameter();
            return parameter != null && extraParameter != null ?
             TryBuild(() => new ParametersRule(parameter, extraParameter)) : null;
        }
        private ExtraParameterRule ParseExtraParameter()
        {
            var comma = Match(Token.Comma);
            var parameter = ParseParameter();
            return parameter != null ?
             TryBuild(() => new ExtraParameterRule(comma, parameter)) : null;
        }

    }
}
