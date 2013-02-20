using System;
using System.Collections.Generic;
using System.Linq;
using Roslyn.Compilers.CSharp;

namespace NAcqt
{
    public static class NodeHelpers
    {
        public static List<T> OfType<T>(this SourceReader reader) where T : SyntaxNode
        {
            var nodes = new List<T>();
            foreach (var tree in reader.SyntaxTrees.Values)
            {
                nodes.AddRange(tree.GetRoot().DescendantNodes().OfType<T>());
            }

            return nodes;
        }

        public static List<T> OfType<T>(this SyntaxTree tree) where T : SyntaxNode
        {
            return tree.GetRoot().DescendantNodes().OfType<T>().ToList();
        }

        public static List<SyntaxNode> Usages(this SourceReader reader, List<ClassDeclarationSyntax> syntaxNodes)
        {
            var nodes = new List<SyntaxNode>();
            foreach (var tree in reader.SyntaxTrees.Values)
            {
                nodes.AddRange(tree.GetRoot().DescendantNodes().Where(n => syntaxNodes.Any(sn => sn.Equals(n))));
            }

            return nodes;
        }

        public static bool StartsWithLowerCase(this string input)
        {
            if (String.IsNullOrEmpty(input))
                return false;

            return "abcdefghijklmnopqrstuvwxyz".Contains(input[0]);
        }

        public static bool StartsWithUpperCase(this string input)
        {
            if (String.IsNullOrEmpty(input))
                return false;

            return "ABCDEFGHIJKLMNOPQRSTUVWXYZ".Contains(input[0]);
        }
    }
}