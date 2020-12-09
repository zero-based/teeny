using System;
using System.Text;
using System.Text.RegularExpressions;
using Teeny.Core.Attributes;

namespace Teeny.Core
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

                // Notify for change if the state is notifiable and there's a lexeme to notify for
                var isNotifiable = _stateType.GetAttributeOfType<NonNotifiableAttribute>() == null;
                if (isNotifiable && Lexeme.Length > 0) OnStateChanged(Lexeme); 

                // Update type and start a new lexeme
                _stateType = value;
                Lexeme.Clear();
            }
        }

        public Action<StringBuilder> OnStateChanged { get; set; }

        public void Update(string scanFrame)
        {
            var isStream = _stateType.GetAttributeOfType<StreamAttribute>() != null;
            if (isStream)
            {
                Lexeme.Append(scanFrame[1]);

                // Close stream if it's eligible for closure
                if (StateType == ScannerStateType.ScanString  && scanFrame[1] == '\"' ||
                    StateType == ScannerStateType.ScanComment && scanFrame.Substring(0, 2) == "*/")
                {
                    StateType = ScannerStateType.CloseStream;
                }

                return;
            }

            StateType = GetStateType(scanFrame);
            Lexeme.Append(scanFrame[1]);
        }

        private static ScannerStateType GetStateType(string scanFrame)
        {
            var stateType = ScannerStateType.Unknown;

            if (scanFrame[1] == '\"')
                stateType = ScannerStateType.ScanString;
            else if (scanFrame.Substring(1, 2) == "/*")
                stateType = ScannerStateType.ScanComment;
            else if (char.IsLetterOrDigit(scanFrame[1]) || scanFrame[1] == '.')
                stateType = ScannerStateType.ScanAlphanumeric;
            else if (char.IsWhiteSpace(scanFrame[1]))
                stateType =  ScannerStateType.ScanWhitespace;
            else if (Regex.IsMatch(scanFrame[1].ToString(), @"[^\w\d\s]"))
                stateType = ScannerStateType.ScanSymbol;

            return stateType;
        }
    }
}
