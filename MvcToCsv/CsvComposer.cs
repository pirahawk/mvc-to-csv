using System;
using System.Linq;

namespace MvcToCsv
{

    public interface ICsvComposer
    {
        /// <summary>
        /// Composes a header row using underlying model metadata
        /// </summary>
        string ComposeHeaderRow(string seperator = null);

        /// <summary>
        /// Creates a Csv row given an instance of the model
        /// </summary>
        string ComposeDataRow(object model, string seperator = null);
    }

    public class CsvComposer : ICsvComposer
    {
        private readonly ICsvModelMetadata _modelMetadata;

        public CsvComposer(ICsvModelMetadata modelMetadata)
        {
            if (modelMetadata == null) throw new ArgumentNullException("modelMetadata");
            _modelMetadata = modelMetadata;
        }

        public string ComposeHeaderRow(string seperator = ",")
        {
            return string.Join(seperator, _modelMetadata.ColumnsToScaffold.Select(col => col.ColumnName));
        }

        public string ComposeDataRow(object model, string seperator = ",")
        {
            return string.Join(seperator,
                _modelMetadata.ColumnsToScaffold.Select(col => col.PropertyValueProvider.ToCsvValue(model)));
        }
    }
}