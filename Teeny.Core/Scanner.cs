using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Teeny.Core.Attributes;

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
            // TODO: Preprocess source code here
            var state = new ScannerState
            {
                OnStateChanged = OnStateChanged
            };

            foreach (var currentChar in sourceCode)
            {
                state.Update(currentChar);
            }

            state.StateType = ScannerStateType.Unknown;

            return TokensTable;
        }

        private void OnStateChanged(StringBuilder lexeme)
        {
            if (lexeme.Length == 0) return;
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

            throw new Exception("Unknown Lexeme");
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