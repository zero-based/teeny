using System.Collections.Generic;
using Teeny.Core.Parse.Rules.Common;
using Teeny.Core.Parse.Rules.Function;
using Teeny.Core.Parse.Rules.Statements;
using Teeny.Core.Scan;

namespace Teeny.Core.Tests.Builders
{
    public class MainFunctionBuilder
    {
        private readonly MainFunctionRule _mainFunctionRule;
        public MainFunctionRule Build => _mainFunctionRule;

        public MainFunctionBuilder()
        {
            _mainFunctionRule = new MainFunctionRule
            {
                DataType = TerminalNodeBuilder.Of(Token.Int),
                Main = TerminalNodeBuilder.Of(Token.Main),
                LeftParenthesis = TerminalNodeBuilder.Of(Token.ParenthesisLeft),
                RightParenthesis = TerminalNodeBuilder.Of(Token.ParenthesisRight),
                FunctionBody = new FunctionBodyRule
                {
                    LeftCurlyBracket = TerminalNodeBuilder.Of(Token.CurlyBracketLeft),
                    RightCurlyBracket = TerminalNodeBuilder.Of(Token.CurlyBracketRight),
                    ReturnStatement = new ReturnStatementRule
                    {
                        Return = TerminalNodeBuilder.Of(Token.Return),
                        Expression = new ExpressionRule
                        {
                            Term = new TermRule
                            {
                                Number = new NumberRule
                                {
                                    Number = TerminalNodeBuilder.Of("0", Token.ConstantNumber)
                                }
                            }
                        },
                        Semicolon = TerminalNodeBuilder.Of(Token.Semicolon)
                    }
                }
            };
        }

        public MainFunctionBuilder WithStatements(ICollection<StatementRule> statementRules)
        {
            _mainFunctionRule.FunctionBody.Statements = statementRules;
            return this;
        }

        public MainFunctionBuilder WithReturnExpression(ExpressionRule expressionRule)
        {
            _mainFunctionRule.FunctionBody.ReturnStatement.Expression = expressionRule;
            return this;
        }
    }
}