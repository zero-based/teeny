using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Teeny.Core.Scan;

namespace Teeny.Core.Tests
{
    [TestClass]
    public class ScannerTest
    {
        private readonly Scanner _scanner = new Scanner();

        [TestMethod]
        public void TestValidIdentifier()
        {
            // Arrange
            const string sourceCode = "int x;";
            var expectedErrors = new List<ErrorRecord>();
            var expectedTokens = new List<TokenRecord>
            {
                new TokenRecord {Lexeme = "int", Token = Token.Int},
                new TokenRecord {Lexeme = "x", Token = Token.Identifier},
                new TokenRecord {Lexeme = ";", Token = Token.Semicolon}
            };

            // Act
            _scanner.Scan(sourceCode);

            // Assert
            CollectionAssert.AreEqual(expectedErrors, _scanner.ErrorTable);
            CollectionAssert.AreEqual(expectedTokens, _scanner.TokensTable);

        }

        [TestMethod]
        public void TestInvalidIdentifier()
        {
            // Arrange
            const string sourceCode = "int 2x;";
            var expectedErrors = new List<ErrorRecord>
            {
                new ErrorRecord {Lexeme = "2x", ErrorType = ErrorType.UnknownToken}
            };

            var expectedTokens = new List<TokenRecord>
            {
                new TokenRecord {Lexeme = "int", Token = Token.Int },
                new TokenRecord {Lexeme = ";", Token = Token.Semicolon },
            };
            // Act
            _scanner.Scan(sourceCode);

            // Assert
            CollectionAssert.AreEqual(expectedErrors, _scanner.ErrorTable);
            CollectionAssert.AreEqual(expectedTokens, _scanner.TokensTable);
        }

        [TestMethod]
        public void TestAssignment()
        {
            // Arrange
            const string sourceCode = "int x:= 3;";
            var expectedErrors = new List<ErrorRecord>();
            var expectedTokens = new List<TokenRecord>
            {
                new TokenRecord {Lexeme = "int", Token = Token.Int},
                new TokenRecord {Lexeme = "x", Token = Token.Identifier},
                new TokenRecord {Lexeme = ":=", Token = Token.Assignment},
                new TokenRecord {Lexeme = "3", Token = Token.ConstantNumber},
                new TokenRecord {Lexeme = ";", Token = Token.Semicolon}
            };

            // Act
            _scanner.Scan(sourceCode);

            // Assert
            CollectionAssert.AreEqual(expectedErrors, _scanner.ErrorTable);
            CollectionAssert.AreEqual(expectedTokens, _scanner.TokensTable);
        }

        [TestMethod]
        public void TestStringComment()
        {
            // Arrange
            const string sourceCode = "\"/Not A comment/\"";
            var expectedErrors = new List<ErrorRecord>();
            var expectedTokens = new List<TokenRecord>
            {
                new TokenRecord {Lexeme = "\"/Not A comment/\"", Token = Token.ConstantString}
            };

            // Act
            _scanner.Scan(sourceCode);

            // Assert
            CollectionAssert.AreEqual(expectedErrors, _scanner.ErrorTable);
            CollectionAssert.AreEqual(expectedTokens, _scanner.TokensTable);
        }

        [TestMethod]
        public void TestConstantNumber()
        {
            // Arrange
            const string sourceCode = "333";
            var expectedErrors = new List<ErrorRecord>();
            var expectedTokens = new List<TokenRecord>
            {
                new TokenRecord {Lexeme = "333", Token = Token.ConstantNumber}
            };

            // Act
            _scanner.Scan(sourceCode);

            // Assert
            CollectionAssert.AreEqual(expectedErrors, _scanner.ErrorTable);
            CollectionAssert.AreEqual(expectedTokens, _scanner.TokensTable);
        }

        [TestMethod]
        public void TestInvalidConstantNumber1()
        {
            // Arrange
            const string sourceCode = "3E9";
            var expectedErrors = new List<ErrorRecord>
            {
                new ErrorRecord {Lexeme = "3E9", ErrorType = ErrorType.UnknownToken}
            };
            var expectedTokens = new List<TokenRecord>();
            //var expectedTokens

            // Act
            _scanner.Scan(sourceCode);

            // Act
            CollectionAssert.AreEqual(expectedErrors, _scanner.ErrorTable);
            CollectionAssert.AreEqual(expectedTokens, _scanner.TokensTable);
        }

        [TestMethod]
        public void TestInvalidConstantNumber2()
        {
            // Arrange
            const string sourceCode = "0.";
            var expectedErrors = new List<ErrorRecord>
            {
                new ErrorRecord {Lexeme = "0.", ErrorType = ErrorType.UnknownToken}
            };
            var expectedTokens = new List<TokenRecord>();

            // Act
            _scanner.Scan(sourceCode);

            // Act
            CollectionAssert.AreEqual(expectedErrors, _scanner.ErrorTable);
            CollectionAssert.AreEqual(expectedTokens, _scanner.TokensTable);
        }

        [TestMethod]
        public void TestInvalidConstantNumber3()
        {
            // Arrange
            const string sourceCode = "0.x";

            var expectedErrors = new List<ErrorRecord>
            {
                new ErrorRecord {Lexeme = "0.x", ErrorType = ErrorType.UnknownToken}
            };
            var expectedTokens = new List<TokenRecord>();


            // Act
            _scanner.Scan(sourceCode);

            // Act
            CollectionAssert.AreEqual(expectedErrors, _scanner.ErrorTable);
            CollectionAssert.AreEqual(expectedTokens, _scanner.TokensTable);
        }

        [TestMethod]
        public void TestInvalidConstantNumber4()
        {
            // Arrange
            const string sourceCode = "0.0.0";

            var expectedErrors = new List<ErrorRecord>
            {
                new ErrorRecord {Lexeme = "0.0.0", ErrorType = ErrorType.UnknownToken}
            };
            var expectedTokens = new List<TokenRecord>();

            // Act
            _scanner.Scan(sourceCode);

            // Act
            CollectionAssert.AreEqual(expectedErrors, _scanner.ErrorTable);
            CollectionAssert.AreEqual(expectedTokens, _scanner.TokensTable);
        }

        [TestMethod]
        public void TestUnclosedString()
        {
            // Arrange
            const string sourceCode = "\"foo";

            var expectedErrors = new List<ErrorRecord>
            {
                new ErrorRecord {Lexeme = "\"foo", ErrorType = ErrorType.UnclosedString}
            };
            var expectedTokens = new List<TokenRecord>();

            // Act
            _scanner.Scan(sourceCode);

            // Act
            CollectionAssert.AreEqual(expectedErrors, _scanner.ErrorTable);
            CollectionAssert.AreEqual(expectedTokens, _scanner.TokensTable);
        }

        [TestMethod]
        public void TestUnclosedStringEndingWithNewLine()
        {
            // Arrange
            const string sourceCode = @"""foo
                                        int bar := 0;";

            var expectedErrors = new List<ErrorRecord>
            {
                new ErrorRecord {Lexeme = $"\"foo{Environment.NewLine}", ErrorType = ErrorType.UnclosedString}
            };

            var expectedTokens = new List<TokenRecord>()
            {
                new TokenRecord{Lexeme = "int", Token = Token.Int},
                new TokenRecord{Lexeme = "bar", Token = Token.Identifier},
                new TokenRecord{Lexeme = ":=", Token = Token.Assignment},
                new TokenRecord{Lexeme = "0", Token = Token.ConstantNumber},
                new TokenRecord{Lexeme = ";", Token = Token.Semicolon}
            };

            // Act
            _scanner.Scan(sourceCode);

            // Act
            CollectionAssert.AreEqual(expectedErrors, _scanner.ErrorTable);
            CollectionAssert.AreEqual(expectedTokens, _scanner.TokensTable);
        }

        [TestMethod]
        public void TestUnclosedComment()
        {
            // Arrange
            const string sourceCode = "/*";

            var expectedErrors = new List<ErrorRecord>
            {
                new ErrorRecord {Lexeme = "/*", ErrorType = ErrorType.UnclosedComment}
            };

            var expectedTokens = new List<TokenRecord>();

            // Act
            _scanner.Scan(sourceCode);

            // Act
            CollectionAssert.AreEqual(expectedErrors, _scanner.ErrorTable);
            CollectionAssert.AreEqual(expectedTokens, _scanner.TokensTable);
        }

        [TestMethod]
        public void NewLineEndOfFile()
        {
            // Arrange
            const string sourceCode =
                @"int main() {
                    read x;
                    write x;
                    write endl;
                    return 0;
                }

                ";

            // Act
            var expectedErrors = new List<ErrorRecord>();
            var expectedTokens = new List<TokenRecord>
            {
                new TokenRecord {Lexeme = "int", Token = Token.Int},
                new TokenRecord {Lexeme = "main", Token = Token.Main},
                new TokenRecord {Lexeme = "(", Token = Token.ParenthesisLeft},
                new TokenRecord {Lexeme = ")", Token = Token.ParenthesisRight},
                new TokenRecord {Lexeme = "{", Token = Token.CurlyBracketLeft},
                new TokenRecord {Lexeme = "read", Token = Token.Read},
                new TokenRecord {Lexeme = "x", Token = Token.Identifier},
                new TokenRecord {Lexeme = ";", Token = Token.Semicolon},
                new TokenRecord {Lexeme = "write", Token = Token.Write},
                new TokenRecord {Lexeme = "x", Token = Token.Identifier},
                new TokenRecord {Lexeme = ";", Token = Token.Semicolon},
                new TokenRecord {Lexeme = "write", Token = Token.Write},
                new TokenRecord {Lexeme = "endl", Token = Token.Endl},
                new TokenRecord {Lexeme = ";", Token = Token.Semicolon},
                new TokenRecord {Lexeme = "return", Token = Token.Return},
                new TokenRecord {Lexeme = "0", Token = Token.ConstantNumber},
                new TokenRecord {Lexeme = ";", Token = Token.Semicolon},
                new TokenRecord {Lexeme = "}", Token = Token.CurlyBracketRight}
            };

            // Act
            _scanner.Scan(sourceCode);

            // Assert
            CollectionAssert.AreEqual(expectedErrors, _scanner.ErrorTable);
            CollectionAssert.AreEqual(expectedTokens, _scanner.TokensTable);
        }

        [TestMethod]
        public void CompleteSampleTest()
        {
            // Arrange
            const string sourceCode =
                @"int main() {
                    /* Sample program in TINY language - computes factorial */
                    read x; /*input an integer */
                    if 0 < x then /* don't compute if x <= 0 */
                        fact := 1;
                        repeat
                        fact := fact * x;
                        x:= x - 1
                        until x = 0
                        write "" The Factorial is "" ;
                        write fact; /* output factorial of x */
                        write endl;
                    end
                    return 0;
                }";


            // Act
            var expectedErrors = new List<ErrorRecord>();
            var expectedTokens = new List<TokenRecord>
            { 
                new TokenRecord {Lexeme = "int", Token = Token.Int},
                new TokenRecord {Lexeme = "main", Token = Token.Main},
                new TokenRecord {Lexeme = "(", Token = Token.ParenthesisLeft},
                new TokenRecord {Lexeme = ")", Token = Token.ParenthesisRight},
                new TokenRecord {Lexeme = "{", Token = Token.CurlyBracketLeft},
                new TokenRecord {Lexeme = "read", Token = Token.Read},
                new TokenRecord {Lexeme = "x", Token = Token.Identifier},
                new TokenRecord {Lexeme = ";", Token = Token.Semicolon},
                new TokenRecord {Lexeme = "if", Token = Token.If},
                new TokenRecord {Lexeme = "0", Token = Token.ConstantNumber},
                new TokenRecord {Lexeme = "<", Token = Token.LessThan},
                new TokenRecord {Lexeme = "x", Token = Token.Identifier},
                new TokenRecord {Lexeme = "then", Token = Token.Then},
                new TokenRecord {Lexeme = "fact", Token = Token.Identifier},
                new TokenRecord {Lexeme = ":=", Token = Token.Assignment},
                new TokenRecord {Lexeme = "1", Token = Token.ConstantNumber},
                new TokenRecord {Lexeme = ";", Token = Token.Semicolon},
                new TokenRecord {Lexeme = "repeat", Token = Token.Repeat},
                new TokenRecord {Lexeme = "fact", Token = Token.Identifier},
                new TokenRecord {Lexeme = ":=", Token = Token.Assignment},
                new TokenRecord {Lexeme = "fact", Token = Token.Identifier},
                new TokenRecord {Lexeme = "*", Token = Token.Multiply},
                new TokenRecord {Lexeme = "x", Token = Token.Identifier},
                new TokenRecord {Lexeme = ";", Token = Token.Semicolon},
                new TokenRecord {Lexeme = "x", Token = Token.Identifier},
                new TokenRecord {Lexeme = ":=", Token = Token.Assignment},
                new TokenRecord {Lexeme = "x", Token = Token.Identifier},
                new TokenRecord {Lexeme = "-", Token = Token.Minus},
                new TokenRecord {Lexeme = "1", Token = Token.ConstantNumber},
                new TokenRecord {Lexeme = "until", Token = Token.Until},
                new TokenRecord {Lexeme = "x", Token = Token.Identifier},
                new TokenRecord {Lexeme = "=", Token = Token.Equal},
                new TokenRecord {Lexeme = "0", Token = Token.ConstantNumber},
                new TokenRecord {Lexeme = "write", Token = Token.Write},
                new TokenRecord {Lexeme = "\" The Factorial is \"", Token = Token.ConstantString},
                new TokenRecord {Lexeme = ";", Token = Token.Semicolon},
                new TokenRecord {Lexeme = "write", Token = Token.Write},
                new TokenRecord {Lexeme = "fact", Token = Token.Identifier},
                new TokenRecord {Lexeme = ";", Token = Token.Semicolon},
                new TokenRecord {Lexeme = "write", Token = Token.Write},
                new TokenRecord {Lexeme = "endl", Token = Token.Endl},
                new TokenRecord {Lexeme = ";", Token = Token.Semicolon},
                new TokenRecord {Lexeme = "end", Token = Token.End},
                new TokenRecord {Lexeme = "return", Token = Token.Return},
                new TokenRecord {Lexeme = "0", Token = Token.ConstantNumber},
                new TokenRecord {Lexeme = ";", Token = Token.Semicolon},
                new TokenRecord {Lexeme = "}", Token = Token.CurlyBracketRight}
            };

            // Act
            _scanner.Scan(sourceCode);

            // Assert
            CollectionAssert.AreEqual(expectedErrors, _scanner.ErrorTable);
            CollectionAssert.AreEqual(expectedTokens, _scanner.TokensTable);
        }


        [TestMethod]
        public void CompleteSampleTest2_NoErrors()
        {
            // Arrange
            const string sourceCode =
                @"int main()
                {
                    string s := ""number of Iterations = "";
                    write s; 
                    write endl;     
                    float z1 := 3*2*(2+1)/2-5.5;
                    int val;
                    read val;                                                                                
                    if  z1 > 5 || z1 < counter && z1 = 1 then 
                        write z1;
                    else
                        z1 := val;
                    end
                    return 0;
                }";

            var expectedErrors = new List<ErrorRecord>();
            var expectedTokens = new List<TokenRecord>
            {
                new TokenRecord {Lexeme = "int", Token = Token.Int},
                new TokenRecord {Lexeme = "main", Token = Token.Main},
                new TokenRecord {Lexeme = "(", Token = Token.ParenthesisLeft},
                new TokenRecord {Lexeme = ")", Token = Token.ParenthesisRight},
                new TokenRecord {Lexeme = "{", Token = Token.CurlyBracketLeft},
                new TokenRecord {Lexeme = "string", Token = Token.String},
                new TokenRecord {Lexeme = "s", Token = Token.Identifier},
                new TokenRecord {Lexeme = ":=", Token = Token.Assignment},
                new TokenRecord {Lexeme = "\"number of Iterations = \"", Token = Token.ConstantString},
                new TokenRecord {Lexeme = ";", Token = Token.Semicolon},
                new TokenRecord {Lexeme = "write", Token = Token.Write},
                new TokenRecord {Lexeme = "s", Token = Token.Identifier},
                new TokenRecord {Lexeme = ";", Token = Token.Semicolon},
                new TokenRecord {Lexeme = "write", Token = Token.Write},
                new TokenRecord {Lexeme = "endl", Token = Token.Endl},
                new TokenRecord {Lexeme = ";", Token = Token.Semicolon},
                new TokenRecord {Lexeme = "float", Token = Token.Float},
                new TokenRecord {Lexeme = "z1", Token = Token.Identifier},
                new TokenRecord {Lexeme = ":=", Token = Token.Assignment},
                new TokenRecord {Lexeme = "3", Token = Token.ConstantNumber},
                new TokenRecord {Lexeme = "*", Token = Token.Multiply},
                new TokenRecord {Lexeme = "2", Token = Token.ConstantNumber},
                new TokenRecord {Lexeme = "*", Token = Token.Multiply},
                new TokenRecord {Lexeme = "(", Token = Token.ParenthesisLeft},
                new TokenRecord {Lexeme = "2", Token = Token.ConstantNumber},
                new TokenRecord {Lexeme = "+", Token = Token.Plus},
                new TokenRecord {Lexeme = "1", Token = Token.ConstantNumber},
                new TokenRecord {Lexeme = ")", Token = Token.ParenthesisRight},
                new TokenRecord {Lexeme = "/", Token = Token.Divide},
                new TokenRecord {Lexeme = "2", Token = Token.ConstantNumber},
                new TokenRecord {Lexeme = "-", Token = Token.Minus},
                new TokenRecord {Lexeme = "5.5", Token = Token.ConstantNumber},
                new TokenRecord {Lexeme = ";", Token = Token.Semicolon},
                new TokenRecord {Lexeme = "int", Token = Token.Int},
                new TokenRecord {Lexeme = "val", Token = Token.Identifier},
                new TokenRecord {Lexeme = ";", Token = Token.Semicolon},
                new TokenRecord {Lexeme = "read", Token = Token.Read},
                new TokenRecord {Lexeme = "val", Token = Token.Identifier},
                new TokenRecord {Lexeme = ";", Token = Token.Semicolon},
                new TokenRecord {Lexeme = "if", Token = Token.If},
                new TokenRecord {Lexeme = "z1", Token = Token.Identifier},
                new TokenRecord {Lexeme = ">", Token = Token.GreaterThan},
                new TokenRecord {Lexeme = "5", Token = Token.ConstantNumber},
                new TokenRecord {Lexeme = "||", Token = Token.Or},
                new TokenRecord {Lexeme = "z1", Token = Token.Identifier},
                new TokenRecord {Lexeme = "<", Token = Token.LessThan},
                new TokenRecord {Lexeme = "counter", Token = Token.Identifier},
                new TokenRecord {Lexeme = "&&", Token = Token.And},
                new TokenRecord {Lexeme = "z1", Token = Token.Identifier},
                new TokenRecord {Lexeme = "=", Token = Token.Equal},
                new TokenRecord {Lexeme = "1", Token = Token.ConstantNumber},
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

            // Act
            _scanner.Scan(sourceCode);

            // Assert
            CollectionAssert.AreEqual(expectedErrors, _scanner.ErrorTable);
            CollectionAssert.AreEqual(expectedTokens, _scanner.TokensTable);
        }

        [TestMethod]
        public void CompleteSampleTest3_WithErrors()
        {
            // Arrange
            const string sourceCode =
                @"int main()
                {
                    float z # 10.5.2;
                    if  z1 <> .35 then 
                        write ""the val of z1 + 1.5*factorial(5) is =;
                    elseif z1 == 35 then
                        z1 := 0.0"";
                    end
                    /* this not supposed to be error
                    return 0;
                    }
                    */
                    write ""#Love_Compiler;
                    return 0;
                }";

            var expectedErrors = new List<ErrorRecord>
            {
                new ErrorRecord{Lexeme = "#", ErrorType = ErrorType.UnknownToken},
                new ErrorRecord{Lexeme = "10.5.2", ErrorType = ErrorType.UnknownToken},
                new ErrorRecord{Lexeme = ".35", ErrorType = ErrorType.UnknownToken},
                new ErrorRecord{Lexeme = $"\"the val of z1 + 1.5*factorial(5) is =;{Environment.NewLine}", ErrorType = ErrorType.UnclosedString},
                new ErrorRecord{Lexeme = "==", ErrorType = ErrorType.UnknownToken},
                new ErrorRecord{Lexeme = $"\";{Environment.NewLine}", ErrorType = ErrorType.UnclosedString},
                new ErrorRecord{Lexeme = $"\"#Love_Compiler;{Environment.NewLine}", ErrorType = ErrorType.UnclosedString},
            };

            var expectedTokens = new List<TokenRecord>
            {
                new TokenRecord {Lexeme = "int", Token = Token.Int},
                new TokenRecord {Lexeme = "main", Token = Token.Main},
                new TokenRecord {Lexeme = "(", Token = Token.ParenthesisLeft},
                new TokenRecord {Lexeme = ")", Token = Token.ParenthesisRight},
                new TokenRecord {Lexeme = "{", Token = Token.CurlyBracketLeft},
                new TokenRecord {Lexeme = "float", Token = Token.Float},
                new TokenRecord {Lexeme = "z", Token = Token.Identifier},
                new TokenRecord {Lexeme = ";", Token = Token.Semicolon},
                new TokenRecord {Lexeme = "if", Token = Token.If},
                new TokenRecord {Lexeme = "z1", Token = Token.Identifier},
                new TokenRecord {Lexeme = "<>", Token = Token.NotEqual},
                new TokenRecord {Lexeme = "then", Token = Token.Then},
                new TokenRecord {Lexeme = "write", Token = Token.Write},
                new TokenRecord {Lexeme = "elseif", Token = Token.ElseIf},
                new TokenRecord {Lexeme = "z1", Token = Token.Identifier},
                new TokenRecord {Lexeme = "35", Token = Token.ConstantNumber},
                new TokenRecord {Lexeme = "then", Token = Token.Then},
                new TokenRecord {Lexeme = "z1", Token = Token.Identifier},
                new TokenRecord {Lexeme = ":=", Token = Token.Assignment},
                new TokenRecord {Lexeme = "0.0", Token = Token.ConstantNumber},
                new TokenRecord {Lexeme = "end", Token = Token.End},
                new TokenRecord {Lexeme = "write", Token = Token.Write},
                new TokenRecord {Lexeme = "return", Token = Token.Return},
                new TokenRecord {Lexeme = "0", Token = Token.ConstantNumber},
                new TokenRecord {Lexeme = ";", Token = Token.Semicolon},
                new TokenRecord {Lexeme = "}", Token = Token.CurlyBracketRight},
            };


            // Act
            _scanner.Scan(sourceCode);

            // Assert
            CollectionAssert.AreEqual(expectedErrors, _scanner.ErrorTable);
            CollectionAssert.AreEqual(expectedTokens, _scanner.TokensTable);
        }

    }
}