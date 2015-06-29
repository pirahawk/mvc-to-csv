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
        public static ICsvModelMetadata BuildModelMetadata<TModel>() where TModel : class
        {
            var columns = FilterPropertiesToScaffold<TModel>()
                .Select(propertyInfo => new CsvColumnContext(propertyInfo.Name, 
                    propertyInfo, 
                    new PropertyValueProvider<TModel>(propertyInfo), 
                    propertyInfo.ShouldIgnoreFromSerialize(), 
                    propertyInfo.CalculateColumnName()));
            return new CsvModelMetadata(columns);
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