using System;

namespace MvcToCsv
{
    /// <summary>
    /// Specifies the string format to be used when serializing the property value to a csv file
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class CsvFormatAttribute : Attribute
    {
        /// <summary>
        /// The format string to use when serializing the property
        /// </summary>
        public string Format { get; set; }

        public CsvFormatAttribute(string format)
        {
            Format = format;
            if (format == null) throw new ArgumentNullException("format");
        }
    }
}