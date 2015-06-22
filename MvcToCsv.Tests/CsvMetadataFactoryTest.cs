using NUnit.Framework;

namespace MvcToCsv.Tests
{
    [TestFixture]
    public class CsvMetadataFactoryTest
    {
        [Test]
        public void OnlyExtractsPublicInstancePropertiesFromModel()
        {
            var columns = CsvMetadataFactory.BuildModelMetadata<PropertiesTestModel>();

            CollectionAssert.AreEquivalent(columns.Keys, new[] { "InstanceProperty" });
            CollectionAssert.AreNotEquivalent(columns.Keys, new[]
            {
                "StaticProperty",
                "PrivateProperty",
                "InstanceMethod",
            });
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