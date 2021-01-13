using System.Collections.Generic;
using DeepEqual.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Teeny.Core.Parse;
using Teeny.Core.Parse.Rules.Common;
using Teeny.Core.Parse.Rules.Equation;
using Teeny.Core.Parse.Rules.Statements;
using Teeny.Core.Parse.Rules.Statements.Condition;
using Teeny.Core.Parse.Rules.Statements.Declaration;
using Teeny.Core.Parse.Rules.Statements.If;
using Teeny.Core.Scan;
using Teeny.Core.Tests.Builders;

namespace Teeny.Core.Tests
{
    [TestClass]
    public class ParserTest
    {
        private readonly Parser _parser = new Parser();

        [TestMethod]
        [TestCategory("DeclarationStatementRule")]
        [TestCategory("IfStatement")]
        [TestCategory("ElseStatement")]
        public void CompleteSampleTest_WithoutErrors()
        {
            // Arrange
            var tokensTable = new List<TokenRecord>
            {
                new TokenRecord {Lexeme = "int", Token = Token.Int},
                new TokenRecord {Lexeme = "main", Token = Token.Main},
                new TokenRecord {Lexeme = "(", Token = Token.ParenthesisLeft},
                new TokenRecord {Lexeme = ")", Token = Token.ParenthesisRight},
                new TokenRecord {Lexeme = "{", Token = Token.CurlyBracketLeft},
                new TokenRecord {Lexeme = "float", Token = Token.Float},
                new TokenRecord {Lexeme = "z1", Token = Token.Identifier},
                new TokenRecord {Lexeme = ":=", Token = Token.Assignment},
                new TokenRecord {Lexeme = "3", Token = Token.ConstantNumber},
                new TokenRecord {Lexeme = "*", Token = Token.Multiply},
                new TokenRecord {Lexeme = "(", Token = Token.ParenthesisLeft},
                new TokenRecord {Lexeme = "2", Token = Token.ConstantNumber},
                new TokenRecord {Lexeme = "+", Token = Token.Plus},
                new TokenRecord {Lexeme = "1", Token = Token.ConstantNumber},
                new TokenRecord {Lexeme = ")", Token = Token.ParenthesisRight},
                new TokenRecord {Lexeme = "-", Token = Token.Minus},
                new TokenRecord {Lexeme = "5.5", Token = Token.ConstantNumber},
                new TokenRecord {Lexeme = ";", Token = Token.Semicolon},
                new TokenRecord {Lexeme = "if", Token = Token.If},
                new TokenRecord {Lexeme = "z1", Token = Token.Identifier},
                new TokenRecord {Lexeme = ">", Token = Token.GreaterThan},
                new TokenRecord {Lexeme = "5", Token = Token.ConstantNumber},
                new TokenRecord {Lexeme = "then", Token = Token.Then},
                new TokenRecord {Lexeme = "write", Token = Token.Write},
                new TokenRecord {Lexeme = "z1", Token = Token.Identifier},
                new TokenRecord {Lexeme = ";", Token = Token.Semicolon},
                new TokenRecord {Lexeme = "else", Token = Token.Else},
                new TokenRecord {Lexeme = "z1", Token = Token.Identifier},
                new TokenRecord {Lexeme = ":=", Token = Token.Assignment},
                new TokenRecord {Lexeme = "val", Token = Token.Identifier},
                new TokenRecord {Lexeme = ";", Token = Token.Semicolon},
                new TokenRecord {Lexeme = "end", Token = Token.End},
                new TokenRecord {Lexeme = "return", Token = Token.Return},
                new TokenRecord {Lexeme = "0", Token = Token.ConstantNumber},
                new TokenRecord {Lexeme = ";", Token = Token.Semicolon},
                new TokenRecord {Lexeme = "}", Token = Token.CurlyBracketRight}
            };

            var expectedProgram = new ProgramBuilder()
                .WithMain(new MainFunctionBuilder()
                    .WithStatements(
                        new List<StatementRule>
                        {
                            new DeclarationStatementRule
                            {
                                DataType = TerminalNodeBuilder.Of(Token.Float),
                                IdOrAssignment = new IdOrAssignmentRule
                                {
                                    Identifier = TerminalNodeBuilder.Of(Token.Identifier, "z1"),
                                    AssignmentOperator = TerminalNodeBuilder.Of(Token.Assignment),
                                    Expression = new ExpressionRule
                                    {
                                        Equation = new EquationRule
                                        {
                                            Term = new TermBuilder().AsNumber("3").Build,
                                            ExtraEquation = new ExtraEquationRule
                                            {
                                                ArithmeticOperator = TerminalNodeBuilder.Of(Token.Multiply),
                                                Equation = new EquationRule
                                                {
                                                    ParenthesisLeft = TerminalNodeBuilder.Of(Token.ParenthesisLeft),
                                                    Equation = new EquationRule
                                                    {
                                                        Term = new TermBuilder().AsNumber("2").Build,
                                                        ExtraEquation = new ExtraEquationRule
                                                        {
                                                            ArithmeticOperator = TerminalNodeBuilder.Of(Token.Plus),
                                                            Equation = new EquationRule
                                                            {
                                                                Term = new TermBuilder().AsNumber("1").Build
                                                            }
                                                        }
                                                    },
                                                    ParenthesisRight = TerminalNodeBuilder.Of(Token.ParenthesisRight),
                                                    ExtraEquation = new ExtraEquationRule
                                                    {
                                                        ArithmeticOperator = TerminalNodeBuilder.Of(Token.Minus),
                                                        Equation = new EquationRule
                                                        {
                                                            Term = new TermBuilder().AsNumber("5.5").Build
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                },
                                Semicolon = TerminalNodeBuilder.Of(Token.Semicolon)
                            },
                            new IfStatementRule
                            {
                                If = TerminalNodeBuilder.Of(Token.If),
                                ConditionStatement = new ConditionStatementRule
                                {
                                    Condition = new ConditionRule
                                    {
                                        Identifier = TerminalNodeBuilder.Of(Token.Identifier, "z1"),
                                        ConditionOperator = TerminalNodeBuilder.Of(Token.GreaterThan),
                                        Term = new TermBuilder().AsNumber("5").Build
                                    }
                                },
                                Then = TerminalNodeBuilder.Of(Token.Then),
                                Statements = new List<StatementRule>
                                {
                                    new WriteStatementRule
                                    {
                                        Write = TerminalNodeBuilder.Of(Token.Write),
                                        Expression = new ExpressionRule
                                        {
                                            Term = new TermBuilder().AsIdentifier("z1").Build
                                        },
                                        Semicolon = TerminalNodeBuilder.Of(Token.Semicolon)
                                    }
                                },
                                ExtraElseIf = new ExtraElseIfRule
                                {
                                    ElseStatement = new ElseStatementRule
                                    {
                                        Else = TerminalNodeBuilder.Of(Token.Else),
                                        Statements = new List<StatementRule>
                                        {
                                            new AssignmentStatementRule
                                            {
                                                Identifier = TerminalNodeBuilder.Of(Token.Identifier, "z1"),
                                                AssignmentOperator = TerminalNodeBuilder.Of(Token.Assignment),
                                                Expression = new ExpressionRule
                                                {
                                                    Term = new TermBuilder().AsIdentifier("val").Build
                                                },
                                                Semicolon = TerminalNodeBuilder.Of(Token.Semicolon)
                                            }
                                        },
                                        End = TerminalNodeBuilder.Of(Token.End)
                                    }
                                }
                            }
                        }).Build).Build;

            // Act
            _parser.Parse(tokensTable);

            // Assert
            _parser.ProgramRoot.ShouldDeepEqual(expectedProgram);
        }

        [TestMethod]
        [TestCategory("RepeatStatement")]
        [TestCategory("DeclarationStatementRule")]
        public void CompleteSampleTest2_WithoutErrors()
        {
            // Arrange
            var tokensTable = new List<TokenRecord>
            {
                new TokenRecord {Lexeme = "int", Token = Token.Int},
                new TokenRecord {Lexeme = "main", Token = Token.Main},
                new TokenRecord {Lexeme = "(", Token = Token.ParenthesisLeft},
                new TokenRecord {Lexeme = ")", Token = Token.ParenthesisRight},
                new TokenRecord {Lexeme = "{", Token = Token.CurlyBracketLeft},
                new TokenRecord {Lexeme = "int", Token = Token.Int},
                new TokenRecord {Lexeme = "x", Token = Token.Identifier},
                new TokenRecord {Lexeme = ":=", Token = Token.Assignment},
                new TokenRecord {Lexeme = "101", Token = Token.ConstantNumber},
                new TokenRecord {Lexeme = ";", Token = Token.Semicolon},
                new TokenRecord {Lexeme = "repeat", Token = Token.Repeat},
                new TokenRecord {Lexeme = "x", Token = Token.Identifier},
                new TokenRecord {Lexeme = ":=", Token = Token.Assignment},
                new TokenRecord {Lexeme = "x", Token = Token.Identifier},
                new TokenRecord {Lexeme = "-", Token = Token.Minus},
                new TokenRecord {Lexeme = "1", Token = Token.ConstantNumber},
                new TokenRecord {Lexeme = ";", Token = Token.Semicolon},
                new TokenRecord {Lexeme = "until", Token = Token.Until},
                new TokenRecord {Lexeme = "x", Token = Token.Identifier},
                new TokenRecord {Lexeme = "=", Token = Token.Equal},
                new TokenRecord {Lexeme = "0", Token = Token.ConstantNumber},
                new TokenRecord {Lexeme = "return", Token = Token.Return},
                new TokenRecord {Lexeme = "0", Token = Token.ConstantNumber},
                new TokenRecord {Lexeme = ";", Token = Token.Semicolon},
                new TokenRecord {Lexeme = "}", Token = Token.CurlyBracketRight}
            };

            var expectedProgram = new ProgramBuilder()
                .WithMain(new MainFunctionBuilder()
                    .WithStatements(new List<StatementRule>
                    {
                        new DeclarationStatementRule
                        {
                            DataType = TerminalNodeBuilder.Of(Token.Int),
                            IdOrAssignment = new IdOrAssignmentRule
                            {
                                Identifier = TerminalNodeBuilder.Of(Token.Identifier, "x"),
                                AssignmentOperator = TerminalNodeBuilder.Of(Token.Assignment),
                                Expression = new ExpressionRule
                                {
                                    Term = new TermBuilder().AsNumber("101").Build
                                }
                            },
                            Semicolon = TerminalNodeBuilder.Of(Token.Semicolon)
                        },
                        new RepeatStatementRule
                        {
                            Repeat = TerminalNodeBuilder.Of(Token.Repeat),
                            Statements = new List<StatementRule>
                            {
                                new AssignmentStatementRule
                                {
                                    Identifier = TerminalNodeBuilder.Of(Token.Identifier, "x"),
                                    AssignmentOperator = TerminalNodeBuilder.Of(Token.Assignment),
                                    Expression = new ExpressionRule
                                    {
                                        Equation = new EquationRule
                                        {
                                            Term = new TermBuilder().AsIdentifier("x").Build,
                                            ExtraEquation = new ExtraEquationRule
                                            {
                                                ArithmeticOperator = TerminalNodeBuilder.Of(Token.Minus),
                                                Equation = new EquationBuilder()
                                                    .AsNumberTermEquation("1")
                                                    .Build
                                            }
                                        }
                                    },
                                    Semicolon = TerminalNodeBuilder.Of(Token.Semicolon)
                                }
                            },
                            Until = TerminalNodeBuilder.Of(Token.Until),
                            ConditionStatement = new ConditionStatementRule
                            {
                                Condition = new ConditionRule
                                {
                                    Identifier = TerminalNodeBuilder.Of(Token.Identifier, "x"),
                                    ConditionOperator = TerminalNodeBuilder.Of(Token.Equal),
                                    Term = new TermBuilder().AsNumber("0").Build
                                }
                            }
                        }
                    }).Build).Build;

            // Act
            _parser.Parse(tokensTable);

            // Assert
            _parser.ProgramRoot.ShouldDeepEqual(expectedProgram);
            Assert.AreEqual(0, _parser.ErrorList.Count);
        }
    }
}