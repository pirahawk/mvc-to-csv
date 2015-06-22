using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using NUnit.Framework;

namespace MvcToCsv.Tests
{
    [TestFixture]
    public class CsvFileFactoryTest
    {
        [Test]
        public void OnlyExtractsPublicInstancePropertiesFromModel()
        {
            var columns = CsvFileFactory.BuildModelMetadata<PropertiesTestModel>();

            CollectionAssert.AreEquivalent(columns.Keys, new[] { "InstanceProperty" });
            CollectionAssert.AreNotEquivalent(columns.Keys, new[]
            {
                "StaticProperty",
                "PrivateProperty",
                "InstanceMethod",
            });
        }

        IEnumerable<TestCaseData> ScaffoldPropertyTestCases
        {
            get
            {
                var modelType = typeof (ScaffoldingTestModel);
                yield return new TestCaseData(modelType.GetProperty("ShouldScaffold")).Returns(true);
                yield return new TestCaseData(modelType.GetProperty("ShouldScaffoldFromAttribute")).Returns(true);
                yield return new TestCaseData(modelType.GetProperty("ShouldNotScaffold")).Returns(false);
            }
        }


        [Test]
        [TestCaseSource("ScaffoldPropertyTestCases")]
        public bool IdentifiesIfColumnShouldBeIgnoredFromScaffolding(PropertyInfo propertyInfo)
        {
            return propertyInfo.ShouldScaffoldColumn();
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