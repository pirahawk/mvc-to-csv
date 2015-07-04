using System;
using System.Reflection;

namespace MvcToCsv
{
    /// <summary>
    /// Represents the meta data associated with a model's property which will be serialized (as a column) within the file
    /// </summary>
    public class CsvColumnContext
    {
        public CsvColumnContext(string propertyName, PropertyInfo propertyInfo, IPropertyValueProvider propertyValueProvider, bool ignoreColumn, string columnName)
        {
            if (columnName == null) throw new ArgumentNullException("columnName");
            if (propertyValueProvider == null) throw new ArgumentNullException("propertyValueProvider");
            if (propertyInfo == null) throw new ArgumentNullException("propertyInfo");
            if (propertyName == null) throw new ArgumentNullException("propertyName");

            ColumnName = columnName;
            PropertyValueProvider = propertyValueProvider;
            IgnoreColumn = ignoreColumn;
            PropertyName = propertyName;
            PropertyInfo = propertyInfo;
        }
        /// <summary>
        /// A human-readable column name
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// Identifies if a model's property should be ignore while generating the csv file
        /// </summary>
        public bool IgnoreColumn { get;  set; }

        /// <summary>
        /// The reflected name of the model's property
        /// </summary>
        public string PropertyName { get; set; }
        
        /// <summary>
        /// The properties Type metadata
        /// </summary>
        public PropertyInfo PropertyInfo { get; set; }

        /// <summary>
        /// Used to obtain the property value from an instance of the model
        /// </summary>
        public IPropertyValueProvider PropertyValueProvider { get; set; }
    }
}