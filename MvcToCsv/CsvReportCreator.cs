using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcToCsv
{
    class CsvReportCreator
    {
        private readonly ICsvComposer _csvComposer;
        private readonly IReportWriter _writer;
        private const string seperator = ",";

        public CsvReportCreator(ICsvComposer csvComposer, IReportWriter writer)
        {
            if (csvComposer == null) throw new ArgumentNullException("csvComposer");
            if (writer == null) throw new ArgumentNullException("writer");

            _csvComposer = csvComposer;
            _writer = writer;
        }

        public void CreateCsv<TModel>(IEnumerable<TModel> allRows)
        {
            WriteHeaderRow();
            WriteModelData(allRows);
        }

        private void WriteModelData<TModel>(IEnumerable<TModel> allRows)
        {
            foreach (var row in allRows)
            {
                WriteLine(_csvComposer.ComposeDataRow(row,seperator));
            }
        }

        private void WriteHeaderRow()
        {
            WriteLine(_csvComposer.ComposeHeaderRow(seperator));
        }

        private void WriteLine(string composedLine)
        {
            _writer.WriteLine(composedLine);
        }
    }
}