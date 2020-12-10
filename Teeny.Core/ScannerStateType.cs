using Teeny.Core.Attributes;

namespace Teeny.Core
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
