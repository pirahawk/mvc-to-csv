using System.Linq;
using NUnit.Framework;

namespace MvcToCsv.Tests
{
    [TestFixture]
    public class CsvMetadataFactoryTest
    {
        [Test]
        public void OnlyExtractsPublicInstancePropertiesWithGettersFromModel()
        {
            var propertiesToScaffold = CsvMetadataFactory.FilterPropertiesToScaffold<PropertiesTestModel>();
            var shouldContain = new[] { "InstanceProperty" };
            var shouldIgnore = new[]
            {
                 "StaticProperty",
                 "PrivateProperty",
                 "InstanceMethod",
                 "PropertyWithPrivateGetter",
                 "PropertyWithNoGetter",
            };

            Assert.True(shouldContain.All( propertyName => 
                    propertiesToScaffold.Any( pi => pi.Name == propertyName)));

            Assert.True(shouldIgnore.All(propertyName =>
                    propertiesToScaffold.All(pi => pi.Name != propertyName)));

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

            public string PropertyWithPrivateGetter { private get; set; }
            public string PropertyWithNoGetter
            {
                set { }
            }
        }
    }
}