using System;

namespace Teeny.Core.Scan.Attributes
{
    public class RegularStateAttribute : Attribute
    {
        public string Pattern { get; }

        public RegularStateAttribute(string pattern)
        {
            Pattern = pattern;
        }
    }
}