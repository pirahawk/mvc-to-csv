using System;

namespace MvcToCsv
{
    /// <summary>
    /// Allows specifying the name of the CSV column produced when serializing this field
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class CsvColumnNameAttribute : Attribute
    {
        /// <summary>
        /// Column name to be used when serializing this field
        /// </summary>
        public string ColumnName { get; set; }

        public CsvColumnNameAttribute(string columnName)
        {
            ColumnName = columnName;
        }
    }
}
