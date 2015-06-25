using System;
using System.Reflection;

namespace MvcToCsv
{
    class PropertyValueProvider<TModel> : IPropertyValueProvider<TModel> where TModel : class
    {
        private readonly PropertyInfo _propertyInfo;
        private readonly Func<object, string> _serializationFunc;

        public PropertyValueProvider(PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
                throw new ArgumentNullException("propertyInfo");

            _propertyInfo = propertyInfo;
            _serializationFunc = _propertyInfo.GetMethodToUseForSerialization();
        }
        
        public string ToCsvValue(TModel modelInstance)
        {
            if (modelInstance == null)
                throw new ArgumentNullException("modelInstance");

            var propertyValue = _propertyInfo.GetValue(modelInstance);
            return _serializationFunc(propertyValue);
        }

        string IPropertyValueProvider.ToCsvValue(object modelInstance)
        {
            if (modelInstance == null) 
                throw new ArgumentNullException("modelInstance");

            return ToCsvValue(modelInstance as TModel);
        }
    }
}