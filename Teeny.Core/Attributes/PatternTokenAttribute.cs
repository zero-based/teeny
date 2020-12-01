using System;

namespace Teeny.Core.Attributes
{
    public class PatternTokenAttribute : Attribute
    {
        public string Pattern;

        public PatternTokenAttribute(string pattern)
        {
            Pattern = pattern;
        }
    }
}
