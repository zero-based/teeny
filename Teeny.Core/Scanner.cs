using System.Collections.Generic;
using System.Text;

namespace Teeny.Core
{
    public class Scanner
    {
        public List<TokenRecord> TokensTable { get; set; } = new List<TokenRecord>();

        public List<TokenRecord> Scan(string sourceCode)
        {

            var state = new ScannerState
            {
                OnStateChanged = OnStateChanged
            };

            // TODO: Move preprocessing to `Compiler` class
            var preprocessor = new Preprocessor(); 
            var processedCode = preprocessor.Preprocess(sourceCode);

            foreach (var currentChar in processedCode)
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