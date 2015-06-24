using System.Reflection;

namespace MvcToCsv
{
    /// <summary>
    /// Represents the meta data associated with a model's property which will be serialized (as a column) within the file
    /// </summary>
    public class CsvColumnContext
    {
        public PropertyInfo PropertyInfo { get; set; }
        public string Name { get; set; }
        public bool IgnoreColumn { get; set; }
        public IPropertyValueProvider PropertyValueProvider { get; set; }
    }
}