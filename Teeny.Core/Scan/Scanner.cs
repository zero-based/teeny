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
        static Scanner()
        {
            ConstantTokensLookup = AttributesHelper.GetLookUpTable<Token, ConstantTokenAttribute>()
                .ToDictionary(x => x.Value.ReservedWord, x => x.Key);
            PatternTokensLookup = AttributesHelper.GetLookUpTable<Token, PatternTokenAttribute>();
            IgnoredTokensLookup = AttributesHelper.GetLookUpTable<Token, IgnoredTokenAttribute>();
        }

        private static Dictionary<string, Token> ConstantTokensLookup { get; }
        private static Dictionary<Token, PatternTokenAttribute> PatternTokensLookup { get; } 
        private static Dictionary<Token, IgnoredTokenAttribute> IgnoredTokensLookup { get; }

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

            if (IgnoredTokensLookup.ContainsKey(token)) return;

            TokensTable.Add(new TokenRecord
            {
                Lexeme = lexeme,
                Token = token
            });
        }

        private static Token Tokenize(string lexeme)
        {
            var found = ConstantTokensLookup.TryGetValue(lexeme, out var tokenValue);
            if (found) return tokenValue;

            foreach (var (token, patternAttribute) in PatternTokensLookup)
                if (Regex.IsMatch(lexeme, patternAttribute.Pattern))
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
    }
}