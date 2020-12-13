using Teeny.Core.Scan.Attributes;

namespace Teeny.Core.Scan
{
    public enum ScannerStateType
    {
        [NonNotifiable]
        ScanStart,
        ScanEnd,

        ScanAlphanumeric,
        ScanSymbol,

        ScanOpenedBracket,
        ScanClosedBracket,

        [NonNotifiable]
        ScanWhitespace,

        [Stream]
        [NonNotifiable]
        ScanComment,

        [Stream]
        ScanString,

        CloseStream,
        Unknown
    }
}
