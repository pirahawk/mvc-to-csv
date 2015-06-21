using System.ComponentModel.DataAnnotations;
using NUnit.Framework;

namespace MvcToCsv.Tests
{
    [TestFixture]
    public class CsvFileFactoryTest
    {
        [Test]
        public void OnlyExtractsPublicInstancePropertiesFromModel()
        {
            var columns = CsvFileFactory.BuildFileMetadata<PropertiesTestModel>();

            CollectionAssert.AreEquivalent(columns.Keys, new[] { "InstanceProperty" });
            CollectionAssert.AreNotEquivalent(columns.Keys, new[]
            {
                "StaticProperty",
                "PrivateProperty",
                "InstanceMethod",
            });
        }

        [Test]
        public void CanCheckIfColumnIgnoredFromScaffolding()
        {
            var metaData = CsvFileFactory.BuildFileMetadata<ScaffoldingTestModel>();

            Assert.That(metaData["ShouldScaffold"].ShouldScaffoldColumn, Is.True);
            Assert.That(metaData["ShouldScaffoldFromAttribute"].ShouldScaffoldColumn, Is.True);
            Assert.That(metaData["ShouldNotScaffold"].ShouldScaffoldColumn, Is.False);
        }

        class ScaffoldingTestModel
        {
            public int ShouldScaffold { get; set; }

            [ScaffoldColumn(true)]
            public int ShouldScaffoldFromAttribute { get; set; }

            [ScaffoldColumn(false)]
            public int ShouldNotScaffold { get; set; }
        }

        class PropertiesTestModel
        {
            public static int StaticProperty { get; set; }
            public int InstanceProperty { get; set; }
            private string PrivateProperty { get; set; }
            public string InstanceMethod()
            {
                return string.Empty;
            }
        }
    }
}