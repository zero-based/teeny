using Teeny.Core.Attributes;

namespace Teeny.Core
{
    public enum ScannerStateType
    {
        // Scan States
        ScanAlphanumeric,
        ScanSymbol,
        ScanWhitespace,
        ScanNumber,

        // Meta States
        [UpdateableBy(CommentEnd)] CommentStart,
        CommentEnd,

        [UpdateableBy(StringEnd)] StringStart,
        StringEnd,

        Unknown
    }
}
