using System.Linq;

namespace Teeny.Core
{
    public class ScanFrame
    {
        public char LookBack { get; }
        public char Center { get; }
        public char LookAhead { get; }

        public ScanFrame(string code, int centerIndex)
        {
            LookBack = code.ElementAtOrDefault(centerIndex - 1);
            Center = code[centerIndex];
            LookAhead = code.ElementAtOrDefault(centerIndex + 1);
        }
    }
}
