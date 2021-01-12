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
using Teeny.Core.Scan;

namespace Teeny.Core.Parse
{
    public class Parser
    {
        private Queue<TokenRecord> _tokensQueue;
        private TokenRecord CurrentRecord => _tokensQueue.Peek();
        private TokenRecord NextRecord => _tokensQueue.ElementAt(1);

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

            return statements != null && returnStatement != null 
                ? TryBuild(() => new FunctionBodyRule(leftCurlyBracket, statements, returnStatement, rightCurlyBracket))
                : null;
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
            return extraArgument != null 
                ? TryBuild(() => new ArgumentsRule(identifier, extraArgument)) 
                : null;
        }

        private FunctionStatementRule ParseFunctionStatement()
        {
            var functionBody = ParseFunctionBody();
            var functionDeclaration = ParseFunctionDeclaration();
 

            return functionBody != null && functionDeclaration != null 
                ? TryBuild(() => new FunctionStatementRule(functionDeclaration, functionBody))
                : null;
        }

        private FunctionCallRule ParseFunctionCall()
        {
            var identifier = Match(Token.Identifier);
            var parenthesisLeft = Match(Token.ParenthesisLeft);
            var arguments = ParseArgumentRule();
            var parenthesisRight = Match(Token.ParenthesisRight);
            return arguments != null
                ? TryBuild(() => new FunctionCallRule(identifier, parenthesisLeft, arguments, parenthesisRight)) 
                : null;
        }

        private ReadStatementRule ParseReadStatement()
        {
            var read = Match(Token.Read);
            var identifier = Match(Token.Identifier);
            var semicolon = Match(Token.Semicolon);
            return TryBuild(() => new ReadStatementRule(read, identifier, semicolon));
        }

        private ReturnStatementRule ParseReturnStatement()
        {
            var @return = Match(Token.Return);
            var expression = ParseExpression();
            var semicolon = Match(Token.Semicolon);

            return expression != null
                ? TryBuild(() => new ReturnStatementRule(@return, expression, semicolon)) 
                : null;
        }

        private AssignmentStatementRule ParseAssignmentStatement()
        {

            var identifier = Match(Token.Identifier);
            var assignment = Match(Token.Assignment);
            var expression = ParseExpression();
            var semicolon = Match(Token.Semicolon);

            return expression != null 
                ? TryBuild(() => new AssignmentStatementRule(identifier, assignment, expression, semicolon)) 
                : null;
        }

        private ParameterRule ParseParameter()
        {
            var dataType = Match(Token.Identifier);
            var identifier = Match(Token.Identifier);
            return TryBuild(() => new ParameterRule(dataType, identifier));
        }

        private ParametersRule ParseParameters()
        {
            var parameter = ParseParameter();
            var extraParameter = ParseExtraParameter();

            return parameter != null && extraParameter != null
                ? TryBuild(() => new ParametersRule(parameter, extraParameter))
                : null;
        }

        private ExtraParameterRule ParseExtraParameter()
        {
            var comma = Match(Token.Comma);
            var parameter = ParseParameter();

            return parameter != null 
                ? TryBuild(() => new ExtraParameterRule(comma, parameter))
                : null;
        }

        private ConditionRule ParseCondition()
        {
            var identifier = Match(Token.Identifier);
            var conditionOperator = Match(Token.LessThan, Token.GreaterThan, Token.Equal, Token.NotEqual);
            var term = ParseTerm();

            return term != null 
                ? TryBuild(() => new ConditionRule(identifier, conditionOperator, term))
                : null;
        }

        private ExtraConditionRule ParseExtraCondition()
        {
            var booleanOperator = Match(Token.And, Token.Or);
            var condition = ParseCondition();
            var extraCondition = ParseExtraCondition();

            return condition != null && extraCondition != null
                ? TryBuild(() => new ExtraConditionRule(booleanOperator, condition, extraCondition))
                : null;
        }

        private RepeatStatementRule ParseRepeatStatement()
        {
            var repeat = Match(Token.Repeat);
            var statements = ParseStatements();
            var until = Match(Token.Until);
            var conditionStatement = ParseConditionStatement();

            return statements != null && conditionStatement != null
                ? TryBuild(() => new RepeatStatementRule(repeat, statements, until, conditionStatement))
                : null;
        }

        private WriteStatementRule ParseWriteStatement()
        {
            var write = Match(Token.Write);
            if (CurrentRecord.Token == Token.Endl)
            {
                var endl = Match(Token.Endl);
                var semicolon = Match(Token.Semicolon);
                return TryBuild(() => new WriteStatementRule(write, endl, semicolon));
            }
            else
            {
                var expression = ParseExpression();
                var semicolon = Match(Token.Semicolon);
                return expression != null
                    ? TryBuild(() => new WriteStatementRule(write, expression, semicolon))
                    : null;
            }
        }

        private ConditionStatementRule ParseConditionStatement()
        {
            var condition = ParseCondition();
            var extraCondition = ParseExtraCondition();

            return condition != null && extraCondition != null
                ? TryBuild(() => new ConditionStatementRule(condition, extraCondition))
                : null;
        }

        private DeclarationStatementRule ParseDeclarationStatement()
        {
            var dataType = Match(Token.Int, Token.Float, Token.String);
            var idOrAssignment = ParseIdOrAssignment();
            var extraIdOrAssign = ParseExtraIdOrAssignment();
            var semicolon = Match(Token.Semicolon);

            return idOrAssignment != null && extraIdOrAssign != null
                ? TryBuild(() => new DeclarationStatementRule(dataType, idOrAssignment, extraIdOrAssign, semicolon))
                : null;
        }
        
        private IfStatementRule ParseIfStatement()
        {
            var @if = Match(Token.If);
            var conditionStatement = ParseConditionStatement();
            var then = Match(Token.Then);
            var statements = ParseStatements();
            var extraElseIf = ParseExtraElseIf();

            return conditionStatement != null && statements != null && extraElseIf != null
                ? TryBuild(() => new IfStatementRule(@if, conditionStatement, then, statements, extraElseIf))
                : null;
        }

        private ElseIfStatementRule ParseElseIfStatement()
        {
            var elseIf = Match(Token.ElseIf);
            var conditionStatement = ParseConditionStatement();
            var then = Match(Token.Then);
            var statements = ParseStatements();
            var extraElseIf = ParseExtraElseIf();

            return conditionStatement != null && statements != null && extraElseIf != null
                ? TryBuild(() => new ElseIfStatementRule(elseIf, conditionStatement, then, statements, extraElseIf))
                : null;
        }

        private ElseStatementRule ParseElseStatement()
        {
            var @else = Match(Token.Else);
            var statements = ParseStatements();
            var end = Match(Token.End);

            return statements != null
                ? TryBuild(() => new ElseStatementRule(@else, statements, end))
                : null;
        }

        private ExtraElseIfRule ParseExtraElseIf()
        {
            switch (CurrentRecord.Token)
            {
                case Token.ElseIf:
                {
                    var elseIfStatement = ParseElseIfStatement();
                    return elseIfStatement != null ? new ExtraElseIfRule(elseIfStatement) : null;
                }
                case Token.Else:
                {
                    var elseStatement = ParseElseIfStatement();
                    return elseStatement != null ? new ExtraElseIfRule(elseStatement) : null;
                }
                default:
                {
                    var end = Match(Token.End);
                    return TryBuild(() => new ExtraElseIfRule(end));
                }
            }
        }

        private IdOrAssignmentRule ParseIdOrAssignment()
        {
            if (CurrentRecord.Token == Token.Identifier && NextRecord.Token == Token.Assignment)
            {
                var assignmentStatement = ParseAssignmentStatement();
                return assignmentStatement != null ? new IdOrAssignmentRule(assignmentStatement) : null;
            }

            var identifier = Match(Token.Identifier);
            return TryBuild(() => new IdOrAssignmentRule(identifier));
        }

        private ExtraIdOrAssignmentRule ParseExtraIdOrAssignment()
        {
            if (CurrentRecord.Token != Token.Comma) return null;

            var comma = Match(Token.Comma);
            var idOrAssignment = ParseIdOrAssignment();
            var extraIdOrAssign = ParseExtraIdOrAssignment();

            return idOrAssignment != null
                ? TryBuild(() => new ExtraIdOrAssignmentRule(comma, idOrAssignment, extraIdOrAssign))
                : null;
        }

        private ExpressionRule ParseExpression()
        {
            if (CurrentRecord.Token != Token.ConstantString)
            {
                var @string = Match(Token.ConstantString);
                return TryBuild(() => new ExpressionRule(@string));
            }

            if (CurrentRecord.Token == Token.ParenthesisLeft
                || NextRecord.Token != Token.Plus
                || NextRecord.Token != Token.Minus
                || NextRecord.Token != Token.Multiply
                || NextRecord.Token != Token.Divide)
            {
                var equation = ParseEquation();
                return equation != null ? new ExpressionRule(equation) : null;
            }

            var term = ParseTerm();
            return term != null ? new ExpressionRule(term) : null;
        }

        private EquationRule ParseEquation()
        {
            if (CurrentRecord.Token == Token.ParenthesisLeft)
            {
                var leftParenthesis = Match(Token.ParenthesisLeft);
                var equation1 = ParseEquation();
                var arithmeticOperator = Match(Token.Plus, Token.Minus, Token.Multiply, Token.Divide);
                var equation2 = ParseEquation();
                var rightParenthesis = Match(Token.ParenthesisRight);
                var extraEquation1 = ParseExtraEquation();

                return equation1 != null && equation2 != null 
                    ? TryBuild(() => new EquationRule(leftParenthesis, equation1, arithmeticOperator, equation2, rightParenthesis, extraEquation1)) 
                    : null;
            }

            var term = ParseTerm();
            var extraEquation2 = ParseExtraEquation();
            return term != null ? new EquationRule(term, extraEquation2) : null;
        }

        private ExtraEquationRule ParseExtraEquation()
        {
            // TODO: Create tokens groups to reuse
            if (CurrentRecord.Token != Token.Plus
                && CurrentRecord.Token != Token.Minus 
                && CurrentRecord.Token != Token.Multiply
                && CurrentRecord.Token != Token.Divide) return null;

            var arithmeticOperator = Match(Token.Plus, Token.Minus, Token.Multiply, Token.Divide);
            var equation = ParseEquation();
            var extraEquation = ParseExtraEquation();

            return equation != null 
                ? TryBuild(() => new ExtraEquationRule(arithmeticOperator, equation, extraEquation))
                : null;
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

        private ICollection<StatementRule> ParseStatements()
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
            var functionStatements = new List<FunctionStatementRule>();
            while (true)
            {
                if (NextRecord.Token == Token.Main) break;
                var functionStatement = ParseFunctionStatement();
                if (functionStatement == null) break;
                functionStatements.Add(functionStatement);
            }

            var mainFunction = ParseMainFunction();
            return mainFunction != null ? new ProgramRule(functionStatements, mainFunction) : null;
        }

        private FunctionDeclarationRule ParseFunctionDeclaration()
        {
            var dataType = Match(Token.Int, Token.Float, Token.String);
            var functionName = Match(Token.Identifier);
            var leftParenthesis = Match(Token.ParenthesisLeft);
            var parameters = ParseParameters();
            var rightParenthesis = Match(Token.ParenthesisRight);


            return parameters != null
                ? TryBuild(() =>
                    new FunctionDeclarationRule(dataType, functionName, leftParenthesis, parameters, rightParenthesis))
                : null;
        }

        private MainFunctionRule ParseMainFunction()
        {
            var dataType = Match(Token.Int);
            var main = Match(Token.Main);
            var leftParenthesis = Match(Token.ParenthesisLeft);
            var rightParenthesis = Match(Token.ParenthesisRight);
            var functionBody = ParseFunctionBody();

            return functionBody != null
                ? TryBuild(() =>
                    new MainFunctionRule(dataType, main, leftParenthesis, rightParenthesis, functionBody))
                : null;
        }
    }
}
