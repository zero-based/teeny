using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Teeny.Core.Attributes;
using Teeny.Core.Exceptions;

namespace Teeny.Core
{
    public class Scanner
    {
        public List<TokenRecord> TokensTable { get; set; } = new List<TokenRecord>();

        public Dictionary<string, Token> ReservedWordsLookup = new Dictionary<string, Token>();
        public Dictionary<string, Token> PatternLookup = new Dictionary<string, Token>();

        public Scanner()
        {
            BuildLookupTables();
        }

        public List<TokenRecord> Scan(string sourceCode)
        {
            var state = new ScannerState
            {
                OnStateChanged = OnStateChanged,
                StateType = ScannerStateType.ScanStart
            };

            for (var i = 0; i < sourceCode.Length; i++)
            {
                var currentChar = sourceCode[i];
                var nextChar = i + 1 <= sourceCode.Length - 1 ? sourceCode[i + 1] : char.MinValue;
                var previousChar = i - 1 >= 0 ? sourceCode[i - 1] : char.MinValue;
                var scanFrame = $"{previousChar}{currentChar}{nextChar}";
                state.Update(scanFrame);
            }

            switch (state.StateType)
            {
                case ScannerStateType.ScanString:
                    throw new UnclosedStringException();
                case ScannerStateType.ScanComment:
                    throw new UnclosedCommentException();
                default:
                    state.StateType = ScannerStateType.ScanEnd;
                    return TokensTable;
            }
        }

        private void OnStateChanged(StringBuilder lexeme)
        {
            var lexemeStr = lexeme.ToString();
            var token = Tokenize(lexemeStr);
            var record = new TokenRecord
            {
                Lexeme = lexemeStr,
                Token = token
            };

            TokensTable.Add(record);
        }

        private Token Tokenize(string lexeme)
        {
            var found = ReservedWordsLookup.TryGetValue(lexeme, out var tokenValue);
            if (found) return tokenValue;

            foreach (var (key, value) in PatternLookup)
            {
                if (Regex.IsMatch(lexeme, key)) return value;
            }

            throw new UnknownLexemeException(lexeme);
        }

        private void BuildLookupTables()
        {
            var tokens = Enum.GetValues(typeof(Token)).Cast<Token>();
            foreach (var token in tokens)
            {
                var reservedAttribute = token.GetAttributeOfType<ConstantTokenAttribute>();
                if (reservedAttribute != null)
                {
                    ReservedWordsLookup.Add(reservedAttribute.ReservedWord, token);
                }

                var patternAttribute = token.GetAttributeOfType<PatternTokenAttribute>();
                if (patternAttribute != null)
                {
                    PatternLookup.Add(patternAttribute.Pattern, token);
                }
            }
        }

    }
}