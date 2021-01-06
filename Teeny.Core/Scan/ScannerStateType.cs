using Teeny.Core.Scan.Attributes;

namespace Teeny.Core.Scan
{
    public enum ScannerStateType
    {
        ScanStart,
        ScanEnd,

        ScanAlphanumeric,
        ScanSymbol,

        ScanOpenedBracket,
        ScanClosedBracket,

        ScanWhitespace,

        [Stream] ScanComment,
        [Stream] ScanString,

        CloseStream,
        Unknown
    }
}