using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcToCsv
{

    public interface ICsvModelMetadata
    {
        /// <summary>
        /// The collection of model properties to scaffold within the Csv file
        /// </summary>
        IEnumerable<CsvColumnContext> ColumnsToScaffold { get; }
    }

    public class CsvModelMetadata : ICsvModelMetadata
    {
        private readonly IEnumerable<CsvColumnContext> _columnMetadata;

        public CsvModelMetadata(IEnumerable<CsvColumnContext> columnMetadata)
        {
            if (columnMetadata == null) throw new ArgumentNullException("columnMetadata");
            _columnMetadata = columnMetadata;
        }

        public IEnumerable<CsvColumnContext> ColumnsToScaffold
        {
            get { return _columnMetadata.Where(cm => !cm.IgnoreColumn); }
        }
    }
}