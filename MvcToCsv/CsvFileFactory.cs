using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace MvcToCsv
{
    public static class CsvFileFactory
    {
        public static IDictionary<string, CsvColumnContext> BuildFileMetadata<TModel>()
        {
            var headerContexts = FilterPropertiesToScaffold<TModel>()
                .ToDictionary(
                    pi => pi.Name,
                    pi => new CsvColumnContext
                    {
                        PropertyInfo = pi,
                        Name = pi.CalculateColumnName(),
                        ShouldScaffoldColumn = pi.ShouldScaffoldColumn()
                    });

            return headerContexts;
        }

        private static bool ShouldScaffoldColumn(this PropertyInfo propertyInfo)
        {
            var scaffoldColumnAttribute = propertyInfo.GetCustomAttribute<ScaffoldColumnAttribute>();
            return scaffoldColumnAttribute == null || scaffoldColumnAttribute.Scaffold;
        }

        private static IEnumerable<PropertyInfo> FilterPropertiesToScaffold<TModel>()
        {
            var modelType = typeof(TModel);
            return modelType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }
    }

    public class CsvColumnContext
    {
        public PropertyInfo PropertyInfo { get; set; }
        public string Name { get; set; }
        public bool ShouldScaffoldColumn { get; set; }
    }
}