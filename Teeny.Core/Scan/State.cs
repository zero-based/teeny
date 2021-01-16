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
        [StreamState(@"(.|\s)/\*", @"\*/(.|\s)")] ScanningComment,
        [StreamState(@"(.|\s)""(.|\s)", @"(.|\s)(""|\n)(.|\s)")] ScanningString,

        // Meta States
        ScanStarted,
        ScanEnded,
        StreamClosed,
        Unknown
    }
}