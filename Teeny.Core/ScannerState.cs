using System;
using System.Text;
using System.Text.RegularExpressions;
using Teeny.Core.Attributes;

namespace Teeny.Core
{
    public class ScannerState
    {
        public StringBuilder Lexeme { get; set; } = new StringBuilder();

        private ScannerStateType _stateType = ScannerStateType.Unknown;

        public ScannerStateType StateType
        {
            get => _stateType;
            set
            {
                var attribute = _stateType.GetAttributeOfType<UpdateableByAttribute>();
                var hasPermission = attribute == null || attribute.UpdatingStateType == value;

                if (_stateType == value || !hasPermission) return;

                OnStateChanged(Lexeme);
                Lexeme.Clear();
                _stateType = value;
            }
        }

        public Action<StringBuilder> OnStateChanged { get; set; }

        public void Update(string scanFrame)
        {

            if (scanFrame[1] == '\"')
            {

                if (StateType != ScannerStateType.StringStart)
                    StateType = ScannerStateType.StringStart;
                else
                {
                    Lexeme.Append('"');
                    StateType = ScannerStateType.StringEnd;
                    return;
                }
            }

            if (scanFrame.Substring(1, 2) == "/*")
                StateType = ScannerStateType.CommentStart;
            else if (scanFrame.Substring(0, 2) == "*/")
                StateType = ScannerStateType.CommentEnd;
            else if (char.IsLetterOrDigit(scanFrame[1]) || scanFrame[1] == '.')
                StateType = ScannerStateType.ScanAlphanumeric;
            else if (char.IsWhiteSpace(scanFrame[1]))
                StateType = ScannerStateType.ScanWhitespace;
            else if (Regex.IsMatch(scanFrame[1].ToString(), @"[^\w\d\s]"))
                StateType = ScannerStateType.ScanSymbol;
            else
                StateType = ScannerStateType.Unknown;

            var isConsumable = _stateType.GetAttributeOfType<ConsumableAttribute>() != null;
            if (isConsumable) return;

            Lexeme.Append(scanFrame[1]);
        }
    }
}
