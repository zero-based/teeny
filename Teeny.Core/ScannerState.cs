﻿using System;
using System.Text;

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
                if (_stateType == value) return;
                OnStateChanged(Lexeme);
                Lexeme.Clear();
                _stateType = value;
            }
        }

        public Action<StringBuilder> OnStateChanged { get; set; }

        public void Update(char currentChar)
        {
            if (char.IsDigit(currentChar))
                StateType = ScannerStateType.ScanNumber;
            else if (char.IsLetterOrDigit(currentChar))
                StateType = ScannerStateType.ScanAlphanumeric;
            else if (char.IsWhiteSpace(currentChar))
                StateType = ScannerStateType.ScanWhitespace;
            else if (char.IsSymbol(currentChar))
                StateType = ScannerStateType.ScanOperator;
            else
                StateType = ScannerStateType.Unknown;

            Lexeme.Append(currentChar);
        }
    }
}
