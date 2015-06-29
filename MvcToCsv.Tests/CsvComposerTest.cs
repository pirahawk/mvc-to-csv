using NUnit.Framework;

namespace MvcToCsv.Tests
{
    [TestFixture]
    public class CsvComposerTest
    {
        [Test]
        public void UsesMetaDataToComposeHeaderRow()
        {
            var csvComposer = new CsvComposer(CsvMetadataFactory.BuildModelMetadata<TestComposerModel>());
            StringAssert.AreEqualIgnoringCase("First,Second", csvComposer.ComposeHeaderRow());
            StringAssert.AreEqualIgnoringCase("First|Second", csvComposer.ComposeHeaderRow("|"));
        }

        [Test]
        public void UsesMetaDataToComposeDataRow()
        {
            var model = new TestComposerModel
            {
                PropOne = 1,
                PropTwo = "fooBar"
            };
            var csvComposer = new CsvComposer(CsvMetadataFactory.BuildModelMetadata<TestComposerModel>());
            StringAssert.AreEqualIgnoringCase("1,fooBar", csvComposer.ComposeDataRow(model));
            StringAssert.AreEqualIgnoringCase("1|fooBar", csvComposer.ComposeDataRow(model, "|"));
        }
    }

    class TestComposerModel
    {
        [CsvColumnName("First")]
        public int PropOne { get; set; }

        [CsvColumnName("Second")]
        public string PropTwo { get; set; }
    }
}