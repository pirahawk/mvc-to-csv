using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace MvcToCsv
{
    public static class CsvColumnNamesUtility
    {

        public static IDictionary<string, CsvHeaderContext> GetPropertyHeaderNames<TModel>()
        {
            var modelType = typeof(TModel);
            var headerContexts = modelType
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .ToDictionary(
                pi => pi.Name,
                pi => new CsvHeaderContext
                {
                    PropertyInfo = pi
                });

            return headerContexts;
        }


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

    public class CsvHeaderContext
    {
        public PropertyInfo PropertyInfo { get; set; }
    }
}