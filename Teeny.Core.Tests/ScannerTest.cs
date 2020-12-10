using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Teeny.Core.Exceptions;

namespace Teeny.Core.Tests
{
    [TestClass]
    public class ScannerTest
    {
        private readonly Comparer<TokenRecord> Comparer = Comparer<TokenRecord>.Create((x, y) => (x.Token.CompareTo(y.Token) + x.Lexeme.CompareTo(y.Lexeme)));
        Scanner scanner = new Scanner();

        [TestMethod]
        public void TestValidIdentifier()
        {
            // Arrange
            var sourceCode = "int x;";
            var expected = new List<TokenRecord>{
                        new TokenRecord {Lexeme = "int", Token = Token.DataTypeInt },
                        new TokenRecord {Lexeme = "x", Token= Token.Identifier },
                        new TokenRecord {Lexeme = ";", Token= Token.SymbolSemicolon }};

            // Act
            var actual = scanner.Scan(sourceCode);

            // Assert
            CollectionAssert.AreEqual(expected, actual, Comparer);

        }

        [TestMethod]
        public void TestInvalidIdentifier()
        {
            // Arrange
            var sourceCode = "int 2x;";

            // Act
            Assert.ThrowsException<UnknownLexemeException>(() => scanner.Scan(sourceCode));

        }

        [TestMethod]
        public void TestAssignment()
        {
            // Arrange
            var sourceCode = "int x:= 3;";
            var expected = new List<TokenRecord>{
                        new TokenRecord {Lexeme = "int", Token = Token.DataTypeInt },
                        new TokenRecord {Lexeme = "x", Token= Token.Identifier },
                        new TokenRecord {Lexeme = ":=", Token= Token.OperatorAssignment },
                        new TokenRecord {Lexeme = "3", Token= Token.ConstantNumber },
                        new TokenRecord {Lexeme = ";", Token= Token.SymbolSemicolon }};

            // Act
            var actual = scanner.Scan(sourceCode);

            // Assert
            CollectionAssert.AreEqual(expected, actual, Comparer);

        }

        [TestMethod]
        public void TestStringComment()
        {
            // Arrange
            var sourceCode = "\"/*Not A comment*/\"";
            var expected = new List<TokenRecord>{
                        new TokenRecord {Lexeme = "\"/*Not A comment*/\"", Token= Token.ConstantString }};

            // Act
            var actual = scanner.Scan(sourceCode);

            // Assert
            CollectionAssert.AreEqual(expected, actual, Comparer);

        }


        [TestMethod]
        public void TestConstantNumber()
        {
            // Arrange
            var sourceCode = "333";
            var expected = new List<TokenRecord>{
                        new TokenRecord {Lexeme = "333", Token= Token.ConstantNumber }};

            // Act
            var actual = scanner.Scan(sourceCode);

            // Assert
            CollectionAssert.AreEqual(expected, actual, Comparer);

        }

        [TestMethod]
        public void TestUnclosedString()
        {
            // Arrange
            var sourceCode = "\"foo";

            // Act
            Assert.ThrowsException<UnclosedStringException>(() => scanner.Scan(sourceCode));

        }

        [TestMethod]
        public void TestUnclosedComment()
        {
            // Arrange
            var sourceCode = "/*";

            // Act
            Assert.ThrowsException<UnclosedCommentException>(() => scanner.Scan(sourceCode));

        }
    }
}
