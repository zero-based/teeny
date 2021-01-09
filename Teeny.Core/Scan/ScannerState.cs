using System;
using System.Text;
using System.Text.RegularExpressions;
using Teeny.Core.Scan.Attributes;

namespace Teeny.Core.Scan
{
    public class ScannerState
    {
        private StringBuilder LexemeBuilder { get; } = new StringBuilder();

        private State _state = State.Unknown;
        public State State
        {
            get => _state;
            set
            {
                if (_state == value) return;

                // Notify for change if there's a lexeme to notify for
                if (LexemeBuilder.Length > 0) OnStateChanged(LexemeBuilder);

                // Update type and start a new lexeme
                _state = value;
                LexemeBuilder.Clear();
            }
        }

        public Action<StringBuilder> OnStateChanged { get; set; }

        public void Update(ScanFrame frame)
        {
            var isStream = _state.GetAttributeOfType<StreamAttribute>() != null;
            if (isStream)
            {
                LexemeBuilder.Append(frame.Center);

                // Close stream if it's eligible for closure
                if (State == State.ScanningString && (frame.Center == '"' || frame.Center == '\n') ||
                    State == State.ScanningComment && frame.Center == '/' && frame.LookBack == '*')
                    State = State.StreamClosed;

                return;
            }

            State = GetState(frame);
            LexemeBuilder.Append(frame.Center);
        }

        private static State GetState(ScanFrame frame)
        {
            var state = State.Unknown;

            if (frame.Center == '"')
                state = State.ScanningString;
            else if (frame.Center == '/' && frame.LookAhead == '*')
                state = State.ScanningComment;
            else if (char.IsLetterOrDigit(frame.Center) || frame.Center == '.')
                state = State.ScanningAlphanumeric;
            else if (char.IsWhiteSpace(frame.Center))
                state = State.ScanningWhitespace;
            else if (frame.Center == '(' || frame.Center == '{' || frame.Center == '[')
                state = State.ScanningLeftBracket;
            else if (frame.Center == ')' || frame.Center == '}' || frame.Center == ']')
                state = State.ScanningRightBracket;
            else if (Regex.IsMatch(frame.Center.ToString(), @"[^\w\d\s]"))
                state = State.ScanningSymbol;

            return state;
        }
    }
}