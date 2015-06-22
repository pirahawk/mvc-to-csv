using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace MvcToCsv
{
    public static class CsvFileFactory
    {
        
        public static IDictionary<string, CsvColumnContext> BuildModelMetadata<TModel>()
        {
            var headerContexts = FilterPropertiesToScaffold<TModel>()
                .ToDictionary(
                    pi => pi.Name,
                    pi => new CsvColumnContext
                    {
                        PropertyInfo = pi,
                        Name = pi.CalculateColumnName(),
                        ScaffoldColumn = pi.ShouldScaffoldColumn()
                    });

            return headerContexts;
        }

        internal static bool ShouldScaffoldColumn(this PropertyInfo propertyInfo)
        {
            if (propertyInfo == null) 
                throw new ArgumentNullException("propertyInfo");

            var scaffoldColumnAttribute = propertyInfo.GetCustomAttribute<ScaffoldColumnAttribute>();
            return scaffoldColumnAttribute == null || scaffoldColumnAttribute.Scaffold;
        }

        internal static IEnumerable<PropertyInfo> FilterPropertiesToScaffold<TModel>()
        {
            var modelType = typeof(TModel);
            return modelType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }
    }
}