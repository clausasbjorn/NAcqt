using System.Linq;
using NAcqt.Demo.Tests.Properties;
using NUnit.Framework;
using Roslyn.Compilers.CSharp;

namespace NAcqt.Demo.Tests
{
    [TestFixture]
    public class Patterns
    {
        private SourceReader _source;

        [SetUp]
        public void SetUp()
        {
            _source = NAcqt.Analyze(Settings.DemoProjectPath);
        }

        [Test]
        public void DependencyInjection()
        {
            var interfaces = _source.OfType<InterfaceDeclarationSyntax>().Select(x => x.Identifier.ToString());

            var classes = _source.OfType<ClassDeclarationSyntax>();
            var implementsInterface =
                classes.Where(
                    c => c.BaseList != null && c.BaseList.Types.Select(t => t.ToString()).Any(t => interfaces.Any(i => i.Contains(t)))).Select(i => i.Identifier.ToString()).ToList();

            var notInjected = _source.OfType<ObjectCreationExpressionSyntax>().Where(o => implementsInterface.Contains(o.Type.ToString()));

            Assert.That(notInjected.Count(), Is.EqualTo(0));
        }
    }
}