using System;

namespace Teeny.Core.Exceptions
{
    public class UnclosedCommentException : Exception
    {
        public UnclosedCommentException()
            : base("Unclosed Comment")
        {
        }
    }
}