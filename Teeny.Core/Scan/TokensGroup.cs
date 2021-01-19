using System.Collections.Generic;
using System.Linq;

namespace Teeny.Core.Scan
{
    internal class TokensGroup : HashSet<Token>
    {
        public static implicit operator Token[](TokensGroup g) => g.ToArray();
    }
}