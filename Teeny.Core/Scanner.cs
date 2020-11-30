using System.Collections.Generic;
using System.Text;

namespace Teeny.Core
{
    public class Scanner
    {
        public List<TokenRecord> TokensTable { get; set; } = new List<TokenRecord>();

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

        private Token Tokenize(string lexeme) => Token.StatementReturn;
    }
}