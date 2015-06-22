using System.Reflection;

namespace MvcToCsv
{
    public class CsvColumnContext
    {
        public PropertyInfo PropertyInfo { get; set; }
        public string Name { get; set; }
        public bool IgnoreColumn { get; set; }
    }
}