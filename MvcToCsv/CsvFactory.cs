using System;
using System.Collections.Generic;

namespace MvcToCsv
{
    /// <summary>
    /// Utility class for converting a collection of models to a CSV file 
    /// </summary>
    public class CsvFactory
    {
        private readonly IReportWriter _writer;

        public CsvFactory(IReportWriter writer)
        {
            if (writer == null) 
                throw new ArgumentNullException("writer");

            _writer = writer;
        }

        /// <summary>
        /// Accepts a collection of models of type <see cref="TModel"/> and converts them to a csv file
        /// </summary>
        public void ToCsv<TModel>(IEnumerable<TModel> allRows) where TModel:class
        {
            if (allRows == null) 
                throw new ArgumentNullException("allRows");

            var csvComposer = new CsvComposer(CsvMetadataFactory.BuildModelMetadata<TModel>());
            var creator = new CsvReportCreator(csvComposer, _writer);
            creator.CreateCsv(allRows);
        }
    }
}
