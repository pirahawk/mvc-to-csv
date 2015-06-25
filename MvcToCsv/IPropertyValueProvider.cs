namespace MvcToCsv
{
    public interface IPropertyValueProvider
    {
        /// <summary>
        /// Retreives a serialzied property value from an instance of the model
        /// </summary>
        string ToCsvValue(object modelInstance);
    }

    internal interface IPropertyValueProvider<in TModel> : IPropertyValueProvider
    {
        string ToCsvValue(TModel modelInstance);
    }
}