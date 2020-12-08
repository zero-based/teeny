using Teeny.Core.Attributes;

namespace Teeny.Core
{
    public enum ScannerStateType
    {
        ScanStart,
        ScanEnd,

        ScanAlphanumeric,
        ScanSymbol,

        [Consumable] ScanWhitespace,

        [Consumable] [UpdateableBy(CommentEnd)] CommentStart,
        [Consumable] CommentEnd,

        [UpdateableBy(StringEnd)] StringStart,
        StringEnd,

        Unknown
    }
}
