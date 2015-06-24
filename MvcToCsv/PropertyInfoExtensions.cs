using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace MvcToCsv
{

    internal static class PropertyInfoExtensions
    {
        /// <summary>
        /// Calculates the column name to assign to the Model property by inspecting its <see cref="CsvColumnNameAttribute"/>, <see cref="DisplayAttribute"/> and <see cref="DisplayNameAttribute"/>, attributes
        /// </summary>
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

        /// <summary>
        /// Marks the property as being ignored from serialization if decorated with the <see cref="CsvIgnoreAttribute"/> attribute
        /// </summary>
        internal static bool ShouldIgnoreFromSerialize(this PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
                throw new ArgumentNullException("propertyInfo");

            var ignoreAttribute = propertyInfo.GetCustomAttribute<CsvIgnoreAttribute>();
            return ignoreAttribute != null && ignoreAttribute.Ignore;
        }

        internal static IPropertyValueProvider CreateModelValueProvider<TModel>(this PropertyInfo propertyInfo)
        {
            return new PropertyValueProvider<TModel>(model => propertyInfo.GetValue(model));
        }
    }


    
}