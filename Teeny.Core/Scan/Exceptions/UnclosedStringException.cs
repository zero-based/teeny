using System;

namespace Teeny.Core.Scan.Exceptions
{
    public class UnclosedStringException : Exception
    {
        public UnclosedStringException()
            : base("Unclosed String")
        {
        }
    }
}