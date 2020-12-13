using System;

namespace Teeny.Core.Scan.Attributes
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