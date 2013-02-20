using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using Roslyn.Compilers.CSharp;

namespace NAcqt
{
    public class SourceReader
    {
        private readonly List<string> _sourceFiles;
        private readonly Dictionary<string, SyntaxTree> _syntaxTrees; 

        public List<string> SourceFiles
        {
            get { return _sourceFiles; }
        } 

        public Dictionary<string, SyntaxTree> SyntaxTrees
        {
            get { return _syntaxTrees; }
        }

        public SourceReader(string root)
        {
            _sourceFiles = LocateSourceFiles(root);
            _syntaxTrees = LoadSyntaxTrees(_sourceFiles);
        }

        private SourceReader(List<string> sourceFiles, Dictionary<string, SyntaxTree> syntaxTrees)
        {
            _sourceFiles = sourceFiles;
            _syntaxTrees = syntaxTrees;
        }

        private List<string> LocateSourceFiles(string root)
        {
            var path = GetDirectory(root);

            var projectDefinition = XDocument.Load(root);
            var sourceFiles = projectDefinition.Descendants()
                .Where(n => n.Name.LocalName.Equals("Compile"))
                .Select(n => String.Format("{0}\\{1}", path, n.Attribute("Include").Value))
                .ToList();

            return sourceFiles;
        }

        private string GetDirectory(string file)
        {
            return new FileInfo(file).Directory.FullName;
        }

        private Dictionary<string, SyntaxTree> LoadSyntaxTrees(IEnumerable<string> sourceFiles)
        {
            var syntaxTrees = new Dictionary<string, SyntaxTree>();
            foreach (var sourceFile in sourceFiles)
            {
                if (!syntaxTrees.ContainsKey(sourceFile))
                    syntaxTrees.Add(sourceFile, SyntaxTree.ParseFile(sourceFile));
            }

            return syntaxTrees;
        }

        public SourceReader Limit(string folder)
        {
            var limited = _syntaxTrees.Where(syntaxTree => syntaxTree.Key.ToLower().StartsWith(folder.ToLower())).ToDictionary(syntaxTree => syntaxTree.Key, syntaxTree => syntaxTree.Value);
            return new SourceReader(limited.Keys.ToList(), limited);
        }
    }
}