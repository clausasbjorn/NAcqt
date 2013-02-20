using System;
using System.Linq;
using NAcqt.Demo.Tests.Properties;
using NUnit.Framework;
using Roslyn.Compilers.CSharp;

namespace NAcqt.Demo.Tests
{
    [TestFixture]
    public class NamingConventions
    {
        private SourceReader _source;

        [SetUp]
        public void SetUp()
        {
            _source = NAcqt.Analyze(Settings.DemoProjectPath);
        }

        [Test]
        public void Methods_WhenPublic_ShouldNotStartWithLowerCase()
        {
            var methods = _source.OfType<MethodDeclarationSyntax>();
            var publicMethods = methods.Where(m => m.Modifiers.Any(mod => mod.Kind == SyntaxKind.PublicKeyword));

            var withLowerCase = publicMethods.Where(x => x.Identifier.Value.ToString().StartsWithLowerCase()).ToList();
            
            Assert.That(
                withLowerCase.Count(), 
                Is.EqualTo(0), 
                String.Format("Methods with lower case: {0}", String.Join(", ", withLowerCase.Select(m => m.ToString())))
            );
        }
    }
}