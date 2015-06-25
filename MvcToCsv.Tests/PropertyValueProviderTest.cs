using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace MvcToCsv.Tests
{
    [TestFixture]
    public class PropertyValueProviderTest
    {
        [Test]
        public void CreatesCsvValues()
        {
            const string propOne = "Test";
            const decimal propTwo = 893m;
            var propThree = new[] { 1, 2, 3, };

            var model = new PropertyValueTest
            {
                PropOne = propOne,
                PropTwo = propTwo,
                PropThree = propThree,
            };

            var csvValues = ToCsvValueDictionary(model);

            StringAssert.AreEqualIgnoringCase(propOne, csvValues["PropOne"]);
            StringAssert.AreEqualIgnoringCase(propTwo.ToString(), csvValues["PropTwo"]);
            StringAssert.AreEqualIgnoringCase(propThree.ToString(), csvValues["PropThree"]);
            Assert.IsNullOrEmpty(csvValues["PropFour"]);
        }

        private static Dictionary<string, string> ToCsvValueDictionary<TModel>(TModel model) where TModel:class
        {
            return model.GetType()
                .GetProperties()
                .ToDictionary(pi => pi.Name,
                    pi => new PropertyValueProvider<TModel>(pi).ToCsvValue(model));
        }

        [Test]
        public void ApplysFormatIfPropertyIsString()
        {
            const int propOne = 1024;
            const decimal propTwo = 0.5m;
            DateTime? propThree = DateTime.Parse("01/02/2000");
            var propFive = new[] { 1,2,3};
            var model = new PropertyFormatTest
            {
                PropOne = propOne,
                PropTwo = propTwo,
                PropThree = propThree,
                PropFive = propFive,
            };
            var csvValues = ToCsvValueDictionary(model);

            StringAssert.AreEqualIgnoringCase(propOne.ToString("N3"), csvValues["PropOne"] );
            StringAssert.AreEqualIgnoringCase(propTwo.ToString("P"), csvValues["PropTwo"]);
            StringAssert.AreEqualIgnoringCase(propThree.Value.ToString("dd-MM-yyyy"), csvValues["PropThree"] );
            StringAssert.AreEqualIgnoringCase(propFive.ToString(), csvValues["PropFive"]);
            StringAssert.AreEqualIgnoringCase(string.Empty, csvValues["PropFour"]);
        }

        class PropertyFormatTest
        {
            [CsvFormat("N3")]
            public int PropOne { get; set; }

            [CsvFormat("P")]
            public decimal PropTwo { get; set; }

            [CsvFormat("dd-MM-yyyy")]
            public DateTime? PropThree { get; set; }

            [CsvFormat("dd-MM-yyyy")]
            public DateTime? PropFour { get; set; }

            [CsvFormat("this does not exist")]
            public int[] PropFive { get; set; }
        }

        class PropertyValueTest
        {
            public string PropOne { get; set; }
            public decimal PropTwo { get; set; }
            public IEnumerable<int> PropThree { get; set; }
            public object PropFour { get; set; }
        }
    }
}