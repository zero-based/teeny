using System;
using System.Collections.Generic;
using System.Linq;
using Teeny.Core.Parse.Rules;
using Teeny.Core.Parse.Rules.Common;
using Teeny.Core.Parse.Rules.Equation;
using Teeny.Core.Parse.Rules.Function;
using Teeny.Core.Parse.Rules.Function.Arguments;
using Teeny.Core.Parse.Rules.Function.Parameters;
using Teeny.Core.Parse.Rules.Statements;
using Teeny.Core.Parse.Rules.Statements.Condition;
using Teeny.Core.Parse.Rules.Statements.Declaration;
using Teeny.Core.Parse.Rules.Statements.If;
using Teeny.Core.Parse.Validation;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse
{
    public class Parser
    {
        private List<TokenRecord> _tokensList;
        public List<string> ErrorList;
        public ProgramRule ProgramRoot;
        private TokenRecord CurrentRecord => _tokensList.First();
        private TokenRecord NextRecord => _tokensList.ElementAt(1);

        private TerminalNode Match(params Token[] tokens)
        {
            if (tokens.Contains(CurrentRecord.Token))
            {
                var tokenRecord = CurrentRecord;
                _tokensList.RemoveAt(0);
                return new TerminalNode(tokenRecord);
            }

            if (tokens.Contains(NextRecord.Token))
            {
                _tokensList.RemoveAt(0);
                var tokenRecord = CurrentRecord;
                _tokensList.RemoveAt(0);
                return new TerminalNode(tokenRecord);
            }

            ErrorList.Add(
                $"Expected {string.Join("or", tokens)}, " +
                $"Found {CurrentRecord.Lexeme} ({CurrentRecord.Token})"
            );

            return null;
        }

        private T Validate<T>(T rule) where T : BaseRule
        {
            // TODO: handle other errors
            var errors = rule.Guard.Errors
                .TakeWhile(e => e is NullFieldError)
                .Select(e => $"Missing {e.FieldName}");

            ErrorList.AddRange(errors);
            return rule;
        }

        public void Parse(List<TokenRecord> tokens)
        {
            _tokensList = tokens;
            ErrorList = new List<string>();

            try
            {
                ProgramRoot = ParseProgram();
            }
            catch (InvalidOperationException e)
            {
                // End of code!
                Console.WriteLine(e);
            }
        }

        private TermRule ParseTerm()
        {
            if (TokensGroups.Signs.Contains(CurrentRecord.Token) || 
                CurrentRecord.Token == Token.ConstantNumber)
            {
                var number = ParseNumber();
                return Validate(new TermRule(number));
            }

            if (NextRecord.Token == Token.ParenthesisLeft)
            {
                var functionCall = ParseFunctionCall();
                return Validate(new TermRule(functionCall));
            }

            var identifier = Match(Token.Identifier);
            return Validate(new TermRule(identifier));
        }

        private NumberRule ParseNumber()
        {
            if (TokensGroups.Signs.Contains(CurrentRecord.Token))
            {
                var sign = ParseSign();
                var number = Match(Token.ConstantNumber);
                return Validate(new NumberRule(sign, number));
            }

            var number2 = Match(Token.ConstantNumber);
            return Validate(new NumberRule(number2));
        }

        private SignRule ParseSign()
        {
            var sign = Match(TokensGroups.Signs);
            return Validate(new SignRule(sign));
        }

        private FunctionBodyRule ParseFunctionBody()
        {
            var curlyBracketLeft = Match(Token.CurlyBracketLeft);
            var statements = ParseStatements() ;
            var returnStatement = statements?.Last() is ReturnStatementRule s ? s : null;
            var curlyBracketRight = Match(Token.CurlyBracketRight);

            if (statements?.Last() is ReturnStatementRule) statements.RemoveAt(statements.Count - 1);
            return Validate(new FunctionBodyRule(curlyBracketLeft, statements, returnStatement, curlyBracketRight));
        }

        private ExtraArgumentRule ParseExtraArgument()
        {
            if (CurrentRecord.Token != Token.Comma) return null;

            var comma = Match(Token.Comma);
            var identifier = Match(Token.Identifier);
            return Validate(new ExtraArgumentRule(comma, identifier));
        }

        private ArgumentsRule ParseArgumentsRule()
        {
            if (!TokensGroups.Arguments.Contains(CurrentRecord.Token)) return null;

            var argument = Match(TokensGroups.Arguments);
            var extraArgument = ParseExtraArgument();

            return Validate(new ArgumentsRule(argument, extraArgument));
        }

        private FunctionStatementRule ParseFunctionStatement()
        {
            if (NextRecord.Token == Token.Main) return null;

            var functionDeclaration = ParseFunctionDeclaration();
            var functionBody = ParseFunctionBody();

            return Validate(new FunctionStatementRule(functionDeclaration, functionBody));
        }

        private FunctionCallRule ParseFunctionCall()
        {
            var identifier = Match(Token.Identifier);
            var parenthesisLeft = Match(Token.ParenthesisLeft);
            var arguments = ParseArgumentsRule();
            var parenthesisRight = Match(Token.ParenthesisRight);

            return Validate(new FunctionCallRule(identifier, parenthesisLeft, arguments, parenthesisRight));
        }

        private ReadStatementRule ParseReadStatement()
        {
            var read = Match(Token.Read);
            var identifier = Match(Token.Identifier);
            var semicolon = Match(Token.Semicolon);

            return Validate(new ReadStatementRule(read, identifier, semicolon));
        }

        private ReturnStatementRule ParseReturnStatement()
        {
            var @return = Match(Token.Return);
            var expression = ParseExpression();
            var semicolon = Match(Token.Semicolon);

            return Validate(new ReturnStatementRule(@return, expression, semicolon));
        }

        private AssignmentStatementRule ParseAssignmentStatement()
        {
            var identifier = Match(Token.Identifier);
            var assignment = Match(Token.Assignment);
            var expression = ParseExpression();
            var semicolon = Match(Token.Semicolon);

            return Validate(new AssignmentStatementRule(identifier, assignment, expression, semicolon));
        }

        private ParameterRule ParseParameter()
        {
            var dataType = Match(TokensGroups.DataTypes);
            var identifier = Match(Token.Identifier);

            return Validate(new ParameterRule(dataType, identifier));
        }

        private ParametersRule ParseParameters()
        {
            if (!TokensGroups.DataTypes.Contains(CurrentRecord.Token)) return null;

            var parameter = ParseParameter();
            var extraParameter = ParseExtraParameter();

            return Validate(new ParametersRule(parameter, extraParameter));
        }

        private ExtraParameterRule ParseExtraParameter()
        {
            if (CurrentRecord.Token != Token.Comma) return null;

            var comma = Match(Token.Comma);
            var parameter = ParseParameter();

            return Validate(new ExtraParameterRule(comma, parameter));
        }

        private ConditionRule ParseCondition()
        {
            var identifier = Match(Token.Identifier);
            var conditionOperator = Match(TokensGroups.ConditionOperators);
            var term = ParseTerm();

            return Validate(new ConditionRule(identifier, conditionOperator, term));
        }

        private ExtraConditionRule ParseExtraCondition()
        {
            if (!TokensGroups.BooleanOperators.Contains(CurrentRecord.Token)) return null;

            var booleanOperator = Match(TokensGroups.BooleanOperators);
            var condition = ParseCondition();
            var extraCondition = ParseExtraCondition();

            return Validate(new ExtraConditionRule(booleanOperator, condition, extraCondition));
        }

        private RepeatStatementRule ParseRepeatStatement()
        {
            var repeat = Match(Token.Repeat);
            var statements = ParseStatements();
            var until = Match(Token.Until);
            var conditionStatement = ParseConditionStatement();

            return Validate(new RepeatStatementRule(repeat, statements, until, conditionStatement));
        }

        private WriteStatementRule ParseWriteStatement()
        {
            var write = Match(Token.Write);

            if (CurrentRecord.Token == Token.Endl)
            {
                var endl = Match(Token.Endl);
                var semicolon = Match(Token.Semicolon);
                return Validate(new WriteStatementRule(write, endl, semicolon));
            }

            var expression = ParseExpression();
            var semicolon2 = Match(Token.Semicolon);

            return Validate(new WriteStatementRule(write, expression, semicolon2));
        }

        private ConditionStatementRule ParseConditionStatement()
        {
            var condition = ParseCondition();
            var extraCondition = ParseExtraCondition();

            return Validate(new ConditionStatementRule(condition, extraCondition));
        }

        private DeclarationStatementRule ParseDeclarationStatement()
        {
            var dataType = Match(TokensGroups.DataTypes);
            var idOrAssignment = ParseIdOrAssignment();
            var extraIdOrAssign = ParseExtraIdOrAssignment();
            var semicolon = Match(Token.Semicolon);

            return Validate(new DeclarationStatementRule(dataType, idOrAssignment, extraIdOrAssign, semicolon));
        }

        private IfStatementRule ParseIfStatement()
        {
            var @if = Match(Token.If);
            var conditionStatement = ParseConditionStatement();
            var then = Match(Token.Then);
            var statements = ParseStatements();
            var extraElseIf = ParseExtraElseIf();

            return Validate(new IfStatementRule(@if, conditionStatement, then, statements, extraElseIf));
        }

        private ElseIfStatementRule ParseElseIfStatement()
        {
            var elseIf = Match(Token.ElseIf);
            var conditionStatement = ParseConditionStatement();
            var then = Match(Token.Then);
            var statements = ParseStatements();
            var extraElseIf = ParseExtraElseIf();

            return Validate(new ElseIfStatementRule(elseIf, conditionStatement, then, statements, extraElseIf));
        }

        private ElseStatementRule ParseElseStatement()
        {
            var @else = Match(Token.Else);
            var statements = ParseStatements();
            var end = Match(Token.End);

            return Validate(new ElseStatementRule(@else, statements, end));
        }

        private ExtraElseIfRule ParseExtraElseIf()
        {
            switch (CurrentRecord.Token)
            {
                case Token.ElseIf:
                {
                    var elseIfStatement = ParseElseIfStatement();
                    return Validate(new ExtraElseIfRule(elseIfStatement));
                }
                case Token.Else:
                {
                    var elseStatement = ParseElseStatement();
                    return Validate(new ExtraElseIfRule(elseStatement));
                }
                default:
                {
                    var end = Match(Token.End);
                    return Validate(new ExtraElseIfRule(end));
                }
            }
        }

        private IdOrAssignmentRule ParseIdOrAssignment()
        {
            if (CurrentRecord.Token == Token.Identifier && NextRecord.Token == Token.Assignment)
            {
                var identifier = Match(Token.Identifier);
                var assignment = Match(Token.Assignment);
                var expression = ParseExpression();

                return Validate(new IdOrAssignmentRule(identifier, assignment, expression));
            }

            var identifier2 = Match(Token.Identifier);
            return Validate(new IdOrAssignmentRule(identifier2));
        }

        private ExtraIdOrAssignmentRule ParseExtraIdOrAssignment()
        {
            if (CurrentRecord.Token != Token.Comma) return null;

            var comma = Match(Token.Comma);
            var idOrAssignment = ParseIdOrAssignment();
            var extraIdOrAssign = ParseExtraIdOrAssignment();

            return Validate(new ExtraIdOrAssignmentRule(comma, idOrAssignment, extraIdOrAssign));
        }

        private ExpressionRule ParseExpression()
        {
            if (CurrentRecord.Token == Token.ConstantString)
            {
                var @string = Match(Token.ConstantString);
                return Validate(new ExpressionRule(@string));
            }

            if (CurrentRecord.Token == Token.ParenthesisLeft || 
                TokensGroups.ArithmeticOperators.Contains(NextRecord.Token))
            {
                var equation = ParseEquation();
                return Validate(new ExpressionRule(equation));
            }

            var term = ParseTerm();
            return Validate(new ExpressionRule(term));
        }

        private EquationRule ParseEquation()
        {
            if (CurrentRecord.Token == Token.ParenthesisLeft)
            {
                var leftParenthesis = Match(Token.ParenthesisLeft);
                var equation = ParseEquation();
                var rightParenthesis = Match(Token.ParenthesisRight);
                var extraEquation = ParseExtraEquation();

                return Validate(new EquationRule(leftParenthesis, equation, rightParenthesis, extraEquation));
            }

            var term = ParseTerm();
            var extraEquation2 = ParseExtraEquation();
            return Validate(new EquationRule(term, extraEquation2));
        }

        private ExtraEquationRule ParseExtraEquation()
        {
            if (!TokensGroups.ArithmeticOperators.Contains(CurrentRecord.Token)) return null;

            var arithmeticOperator = Match(TokensGroups.ArithmeticOperators);
            var equation = ParseEquation();
            var extraEquation = ParseExtraEquation();

            return Validate(new ExtraEquationRule(arithmeticOperator, equation, extraEquation));
        }

        private StatementRule ParseStatement()
        {
            switch (CurrentRecord.Token)
            {
                // TODO: Add errors if any statement returned null
                case Token.Int:
                case Token.Float:
                case Token.String:
                    return ParseDeclarationStatement();
                case Token.Identifier:
                    return ParseAssignmentStatement();
                case Token.If:
                    return ParseIfStatement();
                case Token.Repeat:
                    return ParseRepeatStatement();
                case Token.Read:
                    return ParseReadStatement();
                case Token.Write:
                    return ParseWriteStatement();
                case Token.Return:
                    return ParseReturnStatement();
                default:
                    return null;
            }
        }

        private List<StatementRule> ParseStatements()
        {
            var statements = new List<StatementRule>();

            while (true)
            {
                var statement = ParseStatement();
                if (statement == null) break;
                statements.Add(statement);
            }

            return statements.Count != 0 ? statements : null;
        }

        private ProgramRule ParseProgram()
        {
            var functionStatements = ParseFunctionStatements();
            var mainFunction = ParseMainFunction();

            return Validate(new ProgramRule(functionStatements, mainFunction));
        }

        private ICollection<FunctionStatementRule> ParseFunctionStatements()
        {
            var functionStatements = new List<FunctionStatementRule>();

            while (true)
            {
                var functionStatement = ParseFunctionStatement();
                if (functionStatement == null) break;
                functionStatements.Add(functionStatement);
            }

            return functionStatements.Count != 0 ? functionStatements : null;
        }

        private FunctionDeclarationRule ParseFunctionDeclaration()
        {
            var dataType = Match(TokensGroups.DataTypes);
            var functionName = Match(Token.Identifier);
            var leftParenthesis = Match(Token.ParenthesisLeft);
            var parameters = ParseParameters();
            var rightParenthesis = Match(Token.ParenthesisRight);

            return Validate(new FunctionDeclarationRule(dataType, functionName, leftParenthesis, parameters,
                rightParenthesis));
        }

        private MainFunctionRule ParseMainFunction()
        {
            var dataType = Match(Token.Int);
            var main = Match(Token.Main);
            var leftParenthesis = Match(Token.ParenthesisLeft);
            var rightParenthesis = Match(Token.ParenthesisRight);
            var functionBody = ParseFunctionBody();

            return Validate(new MainFunctionRule(dataType, main, leftParenthesis, rightParenthesis, functionBody));
        }
    }
}