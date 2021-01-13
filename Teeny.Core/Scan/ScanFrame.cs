using System.Linq;
using System.Text.RegularExpressions;

namespace Teeny.Core.Scan
{
    public class ScanFrame
    {
        public ScanFrame(string code, int centerIndex)
        {
            LookBack = code.ElementAtOrDefault(centerIndex - 1);
            Center = code[centerIndex];
            LookAhead = code.ElementAtOrDefault(centerIndex + 1);
        }

        public char LookBack { get; }
        public char Center { get; }
        public char LookAhead { get; }

        public override string ToString()
        {
            return $"{LookBack}{Center}{LookAhead}";
        }

        public bool Matches(string pattern)
        {
            return Regex.IsMatch(ToString(), pattern);
        }

        public bool CenterMatches(string pattern)
        {
            return Regex.IsMatch(Center.ToString(), pattern);
        }
    }
}