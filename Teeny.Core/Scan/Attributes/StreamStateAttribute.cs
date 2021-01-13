using System;

namespace Teeny.Core.Scan.Attributes
{
    public class StreamStateAttribute : Attribute
    {
        public string OpenPattern { get; }
        public string ClosePattern { get; }

        public StreamStateAttribute(string openPattern, string closePattern)
        {
            OpenPattern = openPattern;
            ClosePattern = closePattern;
        }
    }
}