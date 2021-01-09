using Teeny.Core.Scan.Attributes;

namespace Teeny.Core.Scan
{
    public enum State
    {
        ScanStarted,
        ScanEnded,

        ScanningAlphanumeric,
        ScanningSymbol,
        ScanningLeftBracket,
        ScanningRightBracket,
        ScanningWhitespace,

        [Stream] ScanningComment,
        [Stream] ScanningString,
        StreamClosed,

        Unknown
    }
}