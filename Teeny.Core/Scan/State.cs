using Teeny.Core.Scan.Attributes;

namespace Teeny.Core.Scan
{
    public enum State
    {
        // Regular States
        [RegularState(@"[a-zA-Z0-9]|\.")] ScanningAlphanumeric,
        [RegularState(@"\s")] ScanningWhitespace,
        [RegularState(@"\{|\[|\(")] ScanningLeftBracket,
        [RegularState(@"\}|\]|\)")] ScanningRightBracket,
        [RegularState(@"[^\w\d\s]")] ScanningSymbol,

        // Stream States
        [StreamState(@"./\*", @"\*/.")] ScanningComment,
        [StreamState(@"."".", @".(""|\n).")] ScanningString,

        // Meta States
        ScanStarted,
        ScanEnded,
        StreamClosed,
        Unknown
    }
}