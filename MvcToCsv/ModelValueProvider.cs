using System;

namespace MvcToCsv
{
    public interface IPropertyValueProvider
    {
        /// <summary>
        /// Retreives a property value from the model
        /// </summary>
        string GetModelPropertyValue(object model);
    }

    internal interface IPropertyValueProvider<in TModel> : IPropertyValueProvider
    {
        string GetModelPropertyValue(TModel model);
    }

    class PropertyValueProvider<TModel> : IPropertyValueProvider<TModel>
    {
        private readonly Func<TModel, object> _propertyValueProviderFunc;

        public PropertyValueProvider(Func<TModel, object> propertyValueProviderFunc)
        {
            if (propertyValueProviderFunc == null) throw new ArgumentNullException("propertyValueProviderFunc");
            _propertyValueProviderFunc = propertyValueProviderFunc;
        }

        public string GetModelPropertyValue(TModel model)
        {
            var propertyValue = _propertyValueProviderFunc(model);
            return propertyValue.ToString();
        }

        string IPropertyValueProvider.GetModelPropertyValue(object model)
        {
            return GetModelPropertyValue((TModel) model);
        }
    }
}