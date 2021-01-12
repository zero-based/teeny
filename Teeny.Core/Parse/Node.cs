using System.Collections.Generic;

namespace Teeny.Core.Parse
{
    public abstract class Node
    {
        public abstract string Name { get; }
        public abstract IEnumerable<Node> Children { get; }
    }
}