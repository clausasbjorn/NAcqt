using System;
using System.Linq;
using NAcqt.Demo.Tests.Properties;
using NUnit.Framework;
using Roslyn.Compilers.CSharp;

namespace NAcqt.Demo.Tests
{
    [TestFixture]
    public class Documentation
    {
        private SourceReader _source;

        [SetUp]
        public void SetUp()
        {
            _source = NAcqt.Analyze(Settings.DemoProjectPath);
        }

        [Test]
        public void Classes_Always_ShouldHaveSummary()
        {
            var classes = _source.OfType<ClassDeclarationSyntax>();
            var noSummary = classes.Where(
                c => !c.GetLeadingTrivia().Any(
                    trivia => 
                        trivia.Kind == SyntaxKind.DocumentationCommentTrivia && 
                        trivia.ToString().Contains("<summary>")
                    )
            ).ToList();

            Assert.That(
                noSummary.Count(),
                Is.EqualTo(0),
                String.Format("These classes do not have summary comments: {0}", String.Join(", ", noSummary.Select(c => c.ToString())))
            );
        }
    }
}