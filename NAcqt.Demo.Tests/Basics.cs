using NAcqt.Demo.Tests.Properties;
using NUnit.Framework;
using Roslyn.Compilers.CSharp;

namespace NAcqt.Demo.Tests
{
    [TestFixture]
    public class Basics
    {
        private SourceReader _source;

        [SetUp]
        public void SetUp()
        {
            _source = NAcqt.Analyze(Settings.DemoProjectPath);
        }

        [Test]
        public void LoadedAndValid()
        {
            Assert.That(_source, Is.Not.Null);
            Assert.That(_source.SyntaxTrees.Count, Is.GreaterThan(0));
        }
    }
}
