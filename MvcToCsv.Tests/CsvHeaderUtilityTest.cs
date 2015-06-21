using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using NUnit.Framework;

namespace MvcToCsv.Tests
{
    [TestFixture]
    public class CsvHeaderUtilityTest
    {
        [Test]
        public void OnlyExtractsPublicInstancePropertiesFromModel()
        {
            var columns = CsvHeaderUtility.GetPropertyHeaderNames<PropertiesTestModel>();

            CollectionAssert.AreEquivalent(columns.Keys, new[] { "InstanceProperty" });
            CollectionAssert.AreNotEquivalent(columns.Keys, new[]
            {
                "StaticProperty",
                "PrivateProperty",
                "InstanceMethod",
            });
        }

        [Test]
        public void ConsidersDisplayAttributeWhenCalculatingColumnName()
        {
            var columns = CsvHeaderUtility.GetPropertyHeaderNames<ColumnNameTestModel>();
            Assert.That(columns["FirstName"].PropertyInfo.CalculateColumnName(), Is.EqualTo("FirstName"));
            Assert.That(columns["LastName"].PropertyInfo.CalculateColumnName(), Is.EqualTo("LastName"));
            Assert.That(columns["FullName"].PropertyInfo.CalculateColumnName(), Is.EqualTo("Full Name"));
            Assert.That(columns["Alias"].PropertyInfo.CalculateColumnName(), Is.EqualTo("Also Known As"));
        }


        class ColumnNameTestModel
        {
            public string FirstName { get; set; }

            [Display(Description = "Display Name not set")]
            public string LastName { get; set; }

            [Display(Name = "Full Name")]
            public string FullName { get; set; }

            [DisplayName("Also Known As")]
            public string Alias { get; set; }
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