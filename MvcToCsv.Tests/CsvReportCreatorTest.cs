using Moq;
using NUnit.Framework;

namespace MvcToCsv.Tests
{
    [TestFixture]
    public class CsvReportCreatorTest
    {
        [Test]
        public void ComposesCsvRowsCorrectly()
        {
            var composer = new Mock<ICsvComposer>();
            composer.Setup(m => m.ComposeHeaderRow(It.IsAny<string>())).Returns(string.Empty).Verifiable();
            composer.Setup(m => m.ComposeDataRow( It.IsAny<object>(), It.IsAny<string>())).Returns(string.Empty).Verifiable();

            var writer = new Mock<IReportWriter>();
            var creator = new CsvReportCreator(composer.Object, writer.Object);

            creator.CreateCsv(new[]{new object()});

            composer.VerifyAll();
            writer.Verify(m => m.WriteLine(It.IsAny<string>()), Times.Exactly(2));
        }
    }
}