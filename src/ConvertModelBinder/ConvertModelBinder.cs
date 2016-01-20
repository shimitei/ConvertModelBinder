using System;
using System.Globalization;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Mvc.ModelBinding
{
    public class ConvertModelBinder : IModelBinder
    {
        public Task<ModelBindingResult> BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType == typeof(decimal)
                || bindingContext.ModelType == typeof(decimal?))
            {
                var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
                if (valueProviderResult == null)
                {
                    return ModelBindingResult.NoResultAsync;
                }
                if (string.IsNullOrEmpty(valueProviderResult.Values))
                {
                    if (bindingContext.ModelType == typeof(decimal?))
                    {
                        decimal? defaultValue = null;
                        return ModelBindingResult.SuccessAsync(bindingContext.ModelName, defaultValue);
                    }
                    else
                    {
                        decimal defaultValue = 0.0M;
                        return ModelBindingResult.SuccessAsync(bindingContext.ModelName, defaultValue);
                    }
                }

                decimal newModel;
                bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);
                try
                {
                    newModel = Convert.ToDecimal(
                        valueProviderResult.Values,
                        CultureInfo.InvariantCulture);
                    return ModelBindingResult.SuccessAsync(bindingContext.ModelName, newModel);
                }
                catch (Exception e)
                {
                    bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, e, bindingContext.ModelMetadata);
                    //customized error message
                    //string displayName = bindingContext.ModelMetadata.DisplayName ?? bindingContext.ModelName;
                    //bindingContext.ModelState.TryAddModelError(bindingContext.ModelName,
                    //    string.Format("not decimal input:{0}", displayName));
                }
                // Were able to find a converter for the type but conversion failed.
                // Tell the model binding system to skip other model binders.
                return ModelBindingResult.FailedAsync(bindingContext.ModelName);
            }
            else if (bindingContext.ModelType == typeof(DateTime)
                || bindingContext.ModelType == typeof(DateTime?))
            {
                var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
                if (valueProviderResult == null)
                {
                    return null;
                }
                if (string.IsNullOrEmpty(valueProviderResult.Values))
                {
                    if (bindingContext.ModelType == typeof(DateTime?))
                    {
                        DateTime? defaultValue = null;
                        return ModelBindingResult.SuccessAsync(bindingContext.ModelName, defaultValue);
                    }
                    else
                    {
                        DateTime defaultValue = new DateTime();
                        return ModelBindingResult.SuccessAsync(bindingContext.ModelName, defaultValue);
                    }
                }

                DateTime newModel;
                bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);
                try
                {
                    newModel = Convert.ToDateTime(
                        valueProviderResult.Values,
                        CultureInfo.InvariantCulture);
                    return ModelBindingResult.SuccessAsync(bindingContext.ModelName, newModel);
                }
                catch (Exception e)
                {
                    bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, e, bindingContext.ModelMetadata);
                    //customized error message
                    //string displayName = bindingContext.ModelMetadata.DisplayName ?? bindingContext.ModelName;
                    //bindingContext.ModelState.TryAddModelError(bindingContext.ModelName,
                    //    string.Format("not DateTime input:{0}", displayName));
                }
                // Were able to find a converter for the type but conversion failed.
                // Tell the model binding system to skip other model binders.
                return ModelBindingResult.FailedAsync(bindingContext.ModelName);
            }

            // Able to resolve a binder type but need a new model instance and that binder cannot create it.
            return ModelBindingResult.NoResultAsync;
        }
    }
}
