using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Teeny.Core.Scan.Attributes;

namespace Teeny.Core.Scan
{
    public class Scanner
    {
        public Scanner()
        {
            BuildLookupTables();
        }

        private Dictionary<string, Token> ConstantTokensLookup { get; } = new Dictionary<string, Token>();
        private List<(string pattern, Token token)> PatternTokensLookup { get; } = new List<(string pattern, Token token)>();
        private HashSet<Token> IgnoredTokenLookup { get; } = new HashSet<Token>();

        public List<TokenRecord> TokensTable { get; } = new List<TokenRecord>();
        public List<ErrorRecord> ErrorTable { get; } = new List<ErrorRecord>();

        public void Scan(string sourceCode)
        {
            var state = new ScannerState
            {
                OnStateChanged = OnStateChanged,
                State = State.ScanStarted
            };

            for (var i = 0; i < sourceCode.Length; i++)
            {
                var frame = new ScanFrame(sourceCode, i);
                state.Update(frame);
            }

            state.State = State.ScanEnded;
        }

        private void OnStateChanged(StringBuilder lexemeBuilder)
        {
            var lexeme = lexemeBuilder.ToString();
            var token = Tokenize(lexeme);

            if (token == Token.Unknown)
            {
                var error = CategorizeError(lexeme);
                ErrorTable.Add(new ErrorRecord
                {
                    Lexeme = lexeme,
                    Error = error
                });

                return;
            }

            if (IgnoredTokenLookup.Contains(token)) return;

            TokensTable.Add(new TokenRecord
            {
                Lexeme = lexeme,
                Token = token
            });
        }

        private Token Tokenize(string lexeme)
        {
            var found = ConstantTokensLookup.TryGetValue(lexeme, out var tokenValue);
            if (found) return tokenValue;

            foreach (var (pattern, token) in PatternTokensLookup)
                if (Regex.IsMatch(lexeme, pattern))
                    return token;

            return Token.Unknown;
        }

        private static Error CategorizeError(string lexeme)
        {
            var error = lexeme[0] switch
            {
                '\"' => Error.UnclosedString,
                '/' when lexeme[1] == '*' => Error.UnclosedComment,
                _ => Error.UnknownToken
            };

            return error;
        }

        private void BuildLookupTables()
        {
            var tokens = Enum.GetValues(typeof(Token)).Cast<Token>();
            foreach (var token in tokens)
            {
                var constantTokenAttribute = token.GetAttributeOfType<ConstantTokenAttribute>();
                if (constantTokenAttribute != null) ConstantTokensLookup.Add(constantTokenAttribute.ReservedWord, token);

                var patternTokenAttribute = token.GetAttributeOfType<PatternTokenAttribute>();
                if (patternTokenAttribute != null) PatternTokensLookup.Add((patternTokenAttribute.Pattern, token));

                var ignoredTokenAttribute = token.GetAttributeOfType<IgnoredTokenAttribute>();
                if (ignoredTokenAttribute != null) IgnoredTokenLookup.Add(token);
            }
        }
    }
}