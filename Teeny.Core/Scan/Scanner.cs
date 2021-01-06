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
        public Dictionary<string, Token> PatternLookup = new Dictionary<string, Token>();

        public Dictionary<string, Token> ReservedWordsLookup = new Dictionary<string, Token>();

        public Scanner()
        {
            BuildLookupTables();
        }

        public List<TokenRecord> TokensTable { get; set; } = new List<TokenRecord>();
        public List<ErrorRecord> ErrorTable { get; set; } = new List<ErrorRecord>();


        public void Scan(string sourceCode){

            var state = new ScannerState
            {
                OnStateChanged = OnStateChanged,
                StateType = ScannerStateType.ScanStart
            };

            for (var i = 0; i < sourceCode.Length; i++)
            {
                var frame = new ScanFrame(sourceCode, i);
                state.Update(frame);
            }

            state.StateType = ScannerStateType.ScanEnd;
        }

        private void OnStateChanged(StringBuilder lexeme)
        {
            var lexemeStr = lexeme.ToString();
            var token = Tokenize(lexemeStr);

            if (token == Token.Unknown)
            {
                var errorType = CategorizeError(lexemeStr);
                ErrorTable.Add(new ErrorRecord
                {
                    Lexeme = lexemeStr,
                    ErrorType = errorType
                });

                return;
            }

            var ignorable = token.GetAttributeOfType<IgnoreAttribute>() != null;
            if (ignorable) return;

            TokensTable.Add(new TokenRecord
            {
                Lexeme = lexemeStr,
                Token = token
            });
        }

        private Token Tokenize(string lexeme)
        {
            var found = ReservedWordsLookup.TryGetValue(lexeme, out var tokenValue);
            if (found) return tokenValue;

            foreach (var (key, value) in PatternLookup)
                if (Regex.IsMatch(lexeme, key))
                    return value;

            return Token.Unknown;
        }

        private ErrorType CategorizeError(string lexeme)
        {
            var errorType = lexeme[0] switch
            {
                '\"' => ErrorType.UnclosedString,
                '/' when lexeme[1] == '*' => ErrorType.UnclosedComment,
                _ => ErrorType.UnknownToken
            };

            return errorType;

        }

        private void BuildLookupTables()
        {
            var tokens = Enum.GetValues(typeof(Token)).Cast<Token>();
            foreach (var token in tokens)
            {
                var reservedAttribute = token.GetAttributeOfType<ConstantTokenAttribute>();
                if (reservedAttribute != null) ReservedWordsLookup.Add(reservedAttribute.ReservedWord, token);

                var patternAttribute = token.GetAttributeOfType<PatternTokenAttribute>();
                if (patternAttribute != null) PatternLookup.Add(patternAttribute.Pattern, token);
            }
        }
    }
}