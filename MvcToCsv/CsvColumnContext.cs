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

        public string ColumnName { get; set; }
        public bool IgnoreColumn { get;  set; }
        public string PropertyName { get; set; }
        public PropertyInfo PropertyInfo { get; set; }
        public IPropertyValueProvider PropertyValueProvider { get; set; }
    }
}