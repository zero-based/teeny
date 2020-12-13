using System;

namespace Teeny.Core.Scan.Attributes
{
    public class ConstantTokenAttribute : Attribute
    {
        public string ReservedWord;

        public ConstantTokenAttribute(string reservedWord)
        {
            ReservedWord = reservedWord;
        }
    }
}
