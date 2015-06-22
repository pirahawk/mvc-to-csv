﻿using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using NUnit.Framework;

namespace MvcToCsv.Tests
{
    [TestFixture]
    public class ColumnNameCreatorTest
    {
        IEnumerable<TestCaseData> CalculateNameTestCases
        {
            get
            {
                var modelType = typeof(ColumnNameTestModel);
                yield return new TestCaseData(modelType.GetProperty("FirstName")).Returns("FirstName");
                yield return new TestCaseData(modelType.GetProperty("LastName")).Returns("LastName");
                yield return new TestCaseData(modelType.GetProperty("FullName")).Returns("Full Name");
                yield return new TestCaseData(modelType.GetProperty("Alias")).Returns("Also Known As");
                yield return new TestCaseData(modelType.GetProperty("Address")).Returns("Street Address");
            }
        }

        [Test]
        [TestCaseSource("CalculateNameTestCases")]
        public string ConsidersDisplayAttributeWhenCalculatingColumnName(PropertyInfo propertyInfo)
        {
            return propertyInfo.CalculateColumnName();
        }

        [Test]
        public void AlwaysGivesPreferenceToCsvColumnNameAttribute()
        {
            var modelType = typeof(ColumnNamePreferenceModel);
            Assert.That(modelType.GetProperty("TestField").CalculateColumnName(), Is.EqualTo("Column Name Attribute"));
        }

        class ColumnNamePreferenceModel
        {
            [Display(Name = "Display Attribute")]
            [DisplayName("Display Name Attribute")]
            [CsvColumnName("Column Name Attribute")]
            public int TestField { get; set; }
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

            [CsvColumnName("Street Address")]
            public string Address { get; set; }
        }

    }
}