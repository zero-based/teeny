using Teeny.Core.Attributes;

namespace Teeny.Core
{
    public enum ScannerStateType
    {
        ScanStart,
        ScanEnd,

        ScanAlphanumeric,
        ScanSymbol,
        ScanWhitespace,
        ScanNumber,

        [UpdateableBy(CommentEnd)] CommentStart,
        CommentEnd,

        [UpdateableBy(StringEnd)] StringStart,
        StringEnd,

        Unknown
    }
}
