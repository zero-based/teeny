using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Teeny.Core.Scan;
using Teeny.Core.Scan.Exceptions;

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
            var expected = new List<TokenRecord>
            {
                new TokenRecord {Lexeme = "int", Token = Token.DataTypeInt},
                new TokenRecord {Lexeme = "x", Token = Token.Identifier},
                new TokenRecord {Lexeme = ";", Token = Token.SymbolSemicolon}
            };

            // Act
            var actual = _scanner.Scan(sourceCode);

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestInvalidIdentifier()
        {
            // Arrange
            const string sourceCode = "int 2x;";

            // Act
            Assert.ThrowsException<UnknownLexemeException>(() => _scanner.Scan(sourceCode));
        }

        [TestMethod]
        public void TestAssignment()
        {
            // Arrange
            const string sourceCode = "int x:= 3;";
            var expected = new List<TokenRecord>
            {
                new TokenRecord {Lexeme = "int", Token = Token.DataTypeInt},
                new TokenRecord {Lexeme = "x", Token = Token.Identifier},
                new TokenRecord {Lexeme = ":=", Token = Token.OperatorAssignment},
                new TokenRecord {Lexeme = "3", Token = Token.ConstantNumber},
                new TokenRecord {Lexeme = ";", Token = Token.SymbolSemicolon}
            };

            // Act
            var actual = _scanner.Scan(sourceCode);

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestStringComment()
        {
            // Arrange
            const string sourceCode = "\"/*Not A comment*/\"";
            var expected = new List<TokenRecord>
            {
                new TokenRecord {Lexeme = "\"/*Not A comment*/\"", Token = Token.ConstantString}
            };

            // Act
            var actual = _scanner.Scan(sourceCode);

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestConstantNumber()
        {
            // Arrange
            const string sourceCode = "333";
            var expected = new List<TokenRecord>
            {
                new TokenRecord {Lexeme = "333", Token = Token.ConstantNumber}
            };

            // Act
            var actual = _scanner.Scan(sourceCode);

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestInvalidConstantNumber1()
        {
            // Arrange
            const string sourceCode = "3E9";

            // Act
            Assert.ThrowsException<UnknownLexemeException>(() => _scanner.Scan(sourceCode));
        }

        [TestMethod]
        public void TestInvalidConstantNumber2()
        {
            // Arrange
            const string sourceCode = "0.";

            // Act
            Assert.ThrowsException<UnknownLexemeException>(() => _scanner.Scan(sourceCode));
        }

        [TestMethod]
        public void TestInvalidConstantNumber3()
        {
            // Arrange
            const string sourceCode = "0.x";

            // Act
            Assert.ThrowsException<UnknownLexemeException>(() => _scanner.Scan(sourceCode));
        }

        [TestMethod]
        public void TestInvalidConstantNumber4()
        {
            // Arrange
            const string sourceCode = "0.0.0";

            // Act
            Assert.ThrowsException<UnknownLexemeException>(() => _scanner.Scan(sourceCode));
        }

        [TestMethod]
        public void TestUnclosedString()
        {
            // Arrange
            const string sourceCode = "\"foo";

            // Act
            Assert.ThrowsException<UnclosedStringException>(() => _scanner.Scan(sourceCode));
        }

        [TestMethod]
        public void TestUnclosedComment()
        {
            // Arrange
            const string sourceCode = "/*";

            // Act
            Assert.ThrowsException<UnclosedCommentException>(() => _scanner.Scan(sourceCode));
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
            var expected = new List<TokenRecord>
            {
                new TokenRecord {Lexeme = "int", Token = Token.DataTypeInt},
                new TokenRecord {Lexeme = "main", Token = Token.Identifier},
                new TokenRecord {Lexeme = "(", Token = Token.ParenthesisLeft},
                new TokenRecord {Lexeme = ")", Token = Token.ParenthesisRight},
                new TokenRecord {Lexeme = "{", Token = Token.CurlyBracketLeft},
                new TokenRecord {Lexeme = "read", Token = Token.StatementRead},
                new TokenRecord {Lexeme = "x", Token = Token.Identifier},
                new TokenRecord {Lexeme = ";", Token = Token.SymbolSemicolon},
                new TokenRecord {Lexeme = "if", Token = Token.StatementIf},
                new TokenRecord {Lexeme = "0", Token = Token.ConstantNumber},
                new TokenRecord {Lexeme = "<", Token = Token.OperatorConditionalLessThan},
                new TokenRecord {Lexeme = "x", Token = Token.Identifier},
                new TokenRecord {Lexeme = "then", Token = Token.StatementThen},
                new TokenRecord {Lexeme = "fact", Token = Token.Identifier},
                new TokenRecord {Lexeme = ":=", Token = Token.OperatorAssignment},
                new TokenRecord {Lexeme = "1", Token = Token.ConstantNumber},
                new TokenRecord {Lexeme = ";", Token = Token.SymbolSemicolon},
                new TokenRecord {Lexeme = "repeat", Token = Token.StatementRepeat},
                new TokenRecord {Lexeme = "fact", Token = Token.Identifier},
                new TokenRecord {Lexeme = ":=", Token = Token.OperatorAssignment},
                new TokenRecord {Lexeme = "fact", Token = Token.Identifier},
                new TokenRecord {Lexeme = "*", Token = Token.OperatorArithmeticMultiply},
                new TokenRecord {Lexeme = "x", Token = Token.Identifier},
                new TokenRecord {Lexeme = ";", Token = Token.SymbolSemicolon},
                new TokenRecord {Lexeme = "x", Token = Token.Identifier},
                new TokenRecord {Lexeme = ":=", Token = Token.OperatorAssignment},
                new TokenRecord {Lexeme = "x", Token = Token.Identifier},
                new TokenRecord {Lexeme = "-", Token = Token.OperatorArithmeticMinus},
                new TokenRecord {Lexeme = "1", Token = Token.ConstantNumber},
                new TokenRecord {Lexeme = "until", Token = Token.StatementUntil},
                new TokenRecord {Lexeme = "x", Token = Token.Identifier},
                new TokenRecord {Lexeme = "=", Token = Token.OperatorConditionalEqual},
                new TokenRecord {Lexeme = "0", Token = Token.ConstantNumber},
                new TokenRecord {Lexeme = "write", Token = Token.StatementWrite},
                new TokenRecord {Lexeme = "\" The Factorial is \"", Token = Token.ConstantString},
                new TokenRecord {Lexeme = ";", Token = Token.SymbolSemicolon},
                new TokenRecord {Lexeme = "write", Token = Token.StatementWrite},
                new TokenRecord {Lexeme = "fact", Token = Token.Identifier},
                new TokenRecord {Lexeme = ";", Token = Token.SymbolSemicolon},
                new TokenRecord {Lexeme = "write", Token = Token.StatementWrite},
                new TokenRecord {Lexeme = "endl", Token = Token.ConstantEndl},
                new TokenRecord {Lexeme = ";", Token = Token.SymbolSemicolon},
                new TokenRecord {Lexeme = "end", Token = Token.StatementEnd},
                new TokenRecord {Lexeme = "return", Token = Token.StatementReturn},
                new TokenRecord {Lexeme = "0", Token = Token.ConstantNumber},
                new TokenRecord {Lexeme = ";", Token = Token.SymbolSemicolon},
                new TokenRecord {Lexeme = "}", Token = Token.CurlyBracketRight}
            };

            // Act
            var actual = _scanner.Scan(sourceCode);

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}