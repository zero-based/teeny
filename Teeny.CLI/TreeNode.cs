using System.Collections.Generic;
using TreeSharp;

namespace Teeny.CLI
{
    public class TreeNode : TreeNode<TreeNode, List<TreeNode>>
    {
        public string Name;
        public override List<TreeNode> ChildNodes { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}