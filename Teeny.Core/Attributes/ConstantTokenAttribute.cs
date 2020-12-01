using System;

namespace Teeny.Core.Attributes
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
