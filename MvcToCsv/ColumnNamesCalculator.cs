using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace MvcToCsv
{
    /// <summary>
    /// Used to calculate the names of each CSV column
    /// </summary>
    public static class ColumnNamesCalculator
    {
        public static string CalculateColumnName(this PropertyInfo propertyInfo)
        {
            var columnName = propertyInfo.GetCustomAttributes().OfType<DisplayNameAttribute>()
                .Select(attr => attr.DisplayName)
                .Union(propertyInfo.GetCustomAttributes().OfType<DisplayAttribute>()
                    .Select(attr => attr.Name))
                .FirstOrDefault(name => !string.IsNullOrWhiteSpace(name));

            return !string.IsNullOrWhiteSpace(columnName)? columnName : propertyInfo.Name;
        }

    }
}