using System;
using System.IO;

namespace MvcToCsv
{
    public class FileStreamReportWriter : IReportWriter
    {
        private readonly StreamWriter _writer;
        public FileStreamReportWriter(StreamWriter writer)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");

            _writer = writer;
        }

        public void WriteLine(string row)
        {
            _writer.WriteLine(row);
        }
    }
}