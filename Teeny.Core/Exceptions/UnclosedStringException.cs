using System;

namespace Teeny.Core.Exceptions
{
    public class UnclosedStringException : Exception
    {
        public UnclosedStringException()
            : base("Unclosed String")
        {
        }
    }
}