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

        /// <summary>
        /// Inspects the Property to identify if it has been decorated with a <see cref="CsvFormatAttribute"/>
        /// </summary>
        internal static bool HasFormatAttribute(this PropertyInfo propertyInfo)
        {
            return propertyInfo.GetCustomAttributes().OfType<CsvFormatAttribute>().Any();
        }

        /// <summary>
        /// Retreive the <see cref="CsvFormatAttribute"/> instance for the property
        /// </summary>
        internal static CsvFormatAttribute GetFormatAttribute(this PropertyInfo propertyInfo)
        {
            return propertyInfo.GetCustomAttributes().OfType<CsvFormatAttribute>().FirstOrDefault();
        }

        /// <summary>
        /// Is the property a Nullable type
        /// </summary>
        internal static bool IsNullable(this PropertyInfo propertyInfo)
        {
            return propertyInfo.PropertyType.IsGenericType 
                && propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        /// <summary>
        /// Resolve the underlying property type if it is a nullable type
        /// </summary>
        internal static Type ResolveTypeToInspect(this PropertyInfo propertyInfo)
        {
            return propertyInfo.IsNullable() ? Nullable.GetUnderlyingType(propertyInfo.PropertyType)
                : propertyInfo.PropertyType;
        }

        /// <summary>
        /// Create a function which can be used to serialize the property instance value
        /// </summary>
        internal static Func<object,string> GetMethodToUseForSerialization(this PropertyInfo propertyInfo)
        {
            const string toStringMethod = "ToString";
            var propType = propertyInfo.ResolveTypeToInspect();
            var toStrWithFormatArg = propType.GetMethods()
                    .Where(mi => mi.Name == toStringMethod)
                    .FirstOrDefault(mi => mi.GetParameters().Any()
                    && mi.GetParameters().Length == 1
                    && mi.GetParameters().Single().ParameterType == typeof(string));

            if (!propertyInfo.HasFormatAttribute() || toStrWithFormatArg == null)
            {
                return o => o==null? string.Empty: o.ToString();
            }

            var format = propertyInfo.GetFormatAttribute().Format;
            return o => o == null ? string.Empty : toStrWithFormatArg.Invoke(o, new object[] { format }) as string;
        }   
    }
}