using System;

namespace Teeny.Core.Scan.Exceptions
{
    public class UnclosedCommentException : Exception
    {
        public UnclosedCommentException()
            : base("Unclosed Comment")
        {
        }
    }
}