using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MvcToCsv
{
    public static class CsvMetadataFactory
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
                        IgnoreColumn = pi.ShouldIgnoreFromSerialize()
                    });

            return headerContexts;
        }

       
        internal static IEnumerable<PropertyInfo> FilterPropertiesToScaffold<TModel>()
        {
            var modelType = typeof(TModel);
            return modelType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }
    }
}