using System;
using System.Text;

namespace Teeny.Core
{
    class Preprocessor
    {
        public string Preprocess(string sourceCode)
        {
            var builder = new StringBuilder(sourceCode);

            var stringLock = false;
            var commentLock = false;

            for (var i = 0; i < builder.Length; i++)
            {
                if (builder[i] == '"') stringLock = !stringLock;

                var isValidIndex = i + 1 <= builder.Length - 1;
                var canLockComment = !stringLock && isValidIndex;

                if (canLockComment)
                {
                    if (builder.ToString(i, 2) == "/*") commentLock = true;
                    if (builder.ToString(i, 2) == "*/")
                    {
                        commentLock = false;
                        builder.Remove(i--, 2);
                    }
                }

                if (commentLock) builder.Remove(i--, 1);
            }

            if (stringLock) throw new Exception("Unclosed string");
            if (commentLock) throw new Exception("Unclosed comment");

            return builder.ToString();
        }
    }
}
