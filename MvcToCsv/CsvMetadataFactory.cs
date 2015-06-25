using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MvcToCsv
{
    public static class CsvMetadataFactory
    {
        /// <summary>
        /// Builds the model metadata by reflecting over its properties
        /// </summary>
        public static IDictionary<string, CsvColumnContext> BuildModelMetadata<TModel>() where TModel:class 
        {
            var headerContexts = FilterPropertiesToScaffold<TModel>()
                .ToDictionary(
                    pi => pi.Name,
                    pi => new CsvColumnContext
                    {
                        PropertyInfo = pi,
                        Name = pi.CalculateColumnName(),
                        PropertyValueProvider = new PropertyValueProvider<TModel>(pi),
                        IgnoreColumn = pi.ShouldIgnoreFromSerialize(),
                    });

            return headerContexts;
        }

       /// <summary>
       /// Filter which of the model type's properties should be serialized
       /// </summary>
        internal static IEnumerable<PropertyInfo> FilterPropertiesToScaffold<TModel>()
        {
            var modelType = typeof(TModel);
            return modelType
                .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty)
                .Where(pi => pi.GetGetMethod(false) != null );
        }
    }
}