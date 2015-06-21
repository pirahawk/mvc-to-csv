using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using NUnit.Framework;

namespace MvcToCsv.Tests
{
    [TestFixture]
    public class ColumnNamesCalculatorTest
    {
        [Test]
        public void ConsidersDisplayAttributeWhenCalculatingColumnName()
        {
            var columns = CsvFileFactory.BuildFileMetadata<ColumnNameTestModel>();
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

    }
}