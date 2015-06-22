using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace MvcToCsv
{
    /// <summary>
    /// Calculates a csv column names for each field of the model
    /// </summary>
    internal static class PropertyInfoExtensions
    {
        internal static string CalculateColumnName(this PropertyInfo propertyInfo)
        {
            var columnName = propertyInfo.GetCustomAttributes().OfType<CsvColumnNameAttribute>()
                .Select(attr => attr.ColumnName)
                .Union(propertyInfo.GetCustomAttributes().OfType<DisplayNameAttribute>()
                .Select(attr => attr.DisplayName))
                .Union(propertyInfo.GetCustomAttributes().OfType<DisplayAttribute>()
                    .Select(attr => attr.Name))
                .FirstOrDefault(name => !string.IsNullOrWhiteSpace(name));

            return !string.IsNullOrWhiteSpace(columnName)? columnName : propertyInfo.Name;
        }

        internal static bool ShouldIgnoreFromSerialize(this PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
                throw new ArgumentNullException("propertyInfo");

            var ignoreAttribute = propertyInfo.GetCustomAttribute<CsvIgnoreAttribute>();
            return ignoreAttribute != null && ignoreAttribute.Ignore;
        }

    }
}