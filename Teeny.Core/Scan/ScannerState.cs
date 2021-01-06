using System;
using System.Text;
using System.Text.RegularExpressions;
using Teeny.Core.Scan.Attributes;

namespace Teeny.Core.Scan
{
    public class ScannerState
    {
        private StringBuilder Lexeme { get; } = new StringBuilder();

        private ScannerStateType _stateType = ScannerStateType.Unknown;
        public ScannerStateType StateType
        {
            get => _stateType;
            set
            {
                if (_stateType == value) return;

                // Notify for change if there's a lexeme to notify for
                if (Lexeme.Length > 0) OnStateChanged(Lexeme);

                // Update type and start a new lexeme
                _stateType = value;
                Lexeme.Clear();
            }
        }

        public Action<StringBuilder> OnStateChanged { get; set; }

        public void Update(ScanFrame frame)
        {
            var isStream = _stateType.GetAttributeOfType<StreamAttribute>() != null;
            if (isStream)
            {
                Lexeme.Append(frame.Center);

                // Close stream if it's eligible for closure
                if (StateType == ScannerStateType.ScanString && (frame.Center == '"' || frame.Center == '\n') ||
                    StateType == ScannerStateType.ScanComment && frame.Center == '/' && frame.LookBack == '*')
                    StateType = ScannerStateType.CloseStream;

                return;
            }

            StateType = GetStateType(frame);
            Lexeme.Append(frame.Center);
        }

        private static ScannerStateType GetStateType(ScanFrame frame)
        {
            var stateType = ScannerStateType.Unknown;

            if (frame.Center == '"')
                stateType = ScannerStateType.ScanString;
            else if (frame.Center == '/' && frame.LookAhead == '*')
                stateType = ScannerStateType.ScanComment;
            else if (char.IsLetterOrDigit(frame.Center) || frame.Center == '.')
                stateType = ScannerStateType.ScanAlphanumeric;
            else if (char.IsWhiteSpace(frame.Center))
                stateType = ScannerStateType.ScanWhitespace;
            else if (frame.Center == '(' || frame.Center == '{' || frame.Center == '[')
                stateType = ScannerStateType.ScanOpenedBracket;
            else if (frame.Center == ')' || frame.Center == '}' || frame.Center == ']')
                stateType = ScannerStateType.ScanClosedBracket;
            else if (Regex.IsMatch(frame.Center.ToString(), @"[^\w\d\s]"))
                stateType = ScannerStateType.ScanSymbol;

            return stateType;
        }
    }
}