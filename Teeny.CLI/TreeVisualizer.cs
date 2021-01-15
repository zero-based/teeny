using System;
using System.Linq;
using Teeny.Core.Parse;

namespace Teeny.CLI
{
    static class TreeVisualizer
    {
        public static void Visualize(Node node, bool isRoot = true, string indent = "", bool isLast = true)
        {
            var marker = isLast ? "└───" : "├───";
            Console.Write(indent);
            Console.Write(isRoot ? "" : marker);
            Console.Write(node.Name);
            Console.WriteLine();

            indent += isLast ? "    " : "│   ";

            if (node.Children == null) return;

            var last = node.Children.LastOrDefault();

            foreach (var child in node.Children)
                Visualize(child, false, isRoot ? "" : indent, child == last);
        }
    }
}
