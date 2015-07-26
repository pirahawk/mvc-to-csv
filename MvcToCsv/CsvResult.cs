using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace MvcToCsv
{
    /// <summary>
    /// Used to serialize a collection of models of type <see cref="TModel"/> to a Csv file and return them in the HttpResponse
    /// </summary>
    public class CsvResult<TModel> : ActionResult where TModel : class
    {
        private readonly string _fileName;
        private readonly IEnumerable<TModel> _models;

        public CsvResult(string fileName, IEnumerable<TModel> models)
        {
            if (fileName == null) throw new ArgumentNullException("fileName");
            if (models == null) throw new ArgumentNullException("models");

            _fileName = fileName;
            _models = models;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var tempFileName = Path.GetTempFileName();
            using (var fileStream = File.Create(tempFileName))
            {
                using (var writer = new StreamWriter(fileStream))
                {
                    var reportWriter = new FileStreamReportWriter(writer);
                    var csvFactory = new CsvFactory(reportWriter);
                    csvFactory.ToCsv(_models);
                }
            }

            using (var fileStream = File.OpenRead(tempFileName))
            {
                new FileStreamResult(fileStream, "text/plain")
                {
                    FileDownloadName = _fileName
                }.ExecuteResult(context);
            }
        }
    }
}