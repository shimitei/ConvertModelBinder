using System;
using System.Globalization;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Mvc.ModelBinding
{
    public class ConvertModelBinder : IModelBinder
    {
        public async Task<ModelBindingResult> BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType == typeof(decimal)
                || bindingContext.ModelType == typeof(decimal?))
            {
                var valueProviderResult = await bindingContext.ValueProvider.GetValueAsync(bindingContext.ModelName);
                if (valueProviderResult == null)
                {
                    return null;
                }
                if (string.IsNullOrEmpty(valueProviderResult.AttemptedValue))
                {
                    if (bindingContext.ModelType == typeof(decimal?))
                    {
                        decimal? defaultValue = null;
                        return new ModelBindingResult(defaultValue, bindingContext.ModelName, isModelSet: true);
                    }
                    else
                    {
                        decimal defaultValue = 0.0M;
                        return new ModelBindingResult(defaultValue, bindingContext.ModelName, isModelSet: true);
                    }
                }

                decimal newModel;
                bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);
                try
                {
                    newModel = Convert.ToDecimal(
                        valueProviderResult.AttemptedValue,
                        CultureInfo.InvariantCulture
                    );
                    return new ModelBindingResult(newModel, bindingContext.ModelName, isModelSet: true);
                }
                catch (Exception e)
                {
                    bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, e);
                    //customized error message
                    //string displayName = bindingContext.ModelMetadata.DisplayName ?? bindingContext.ModelName;
                    //bindingContext.ModelState.TryAddModelError(bindingContext.ModelName,
                    //    string.Format("not decimal input:{0}", displayName));
                }
                // Were able to find a converter for the type but conversion failed.
                // Tell the model binding system to skip other model binders i.e. return non-null.
                return new ModelBindingResult(model: null, key: bindingContext.ModelName, isModelSet: false);
            } else if (bindingContext.ModelType == typeof(DateTime)
                || bindingContext.ModelType == typeof(DateTime?))
            {
                var valueProviderResult = await bindingContext.ValueProvider.GetValueAsync(bindingContext.ModelName);
                if (valueProviderResult == null)
                {
                    return null;
                }
                if (string.IsNullOrEmpty(valueProviderResult.AttemptedValue))
                {
                    if (bindingContext.ModelType == typeof(DateTime?))
                    {
                        DateTime? defaultValue = null;
                        return new ModelBindingResult(defaultValue, bindingContext.ModelName, isModelSet: true);
                    }
                    else
                    {
                        DateTime defaultValue = new DateTime();
                        return new ModelBindingResult(defaultValue, bindingContext.ModelName, isModelSet: true);
                    }
                }

                DateTime newModel;
                bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);
                try
                {
                    newModel = Convert.ToDateTime(
                        valueProviderResult.AttemptedValue,
                        CultureInfo.InvariantCulture
                    );
                    return new ModelBindingResult(newModel, bindingContext.ModelName, isModelSet: true);
                }
                catch (Exception e)
                {
                    bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, e);
                    //customized error message
                    //string displayName = bindingContext.ModelMetadata.DisplayName ?? bindingContext.ModelName;
                    //bindingContext.ModelState.TryAddModelError(bindingContext.ModelName,
                    //    string.Format("not DateTime input:{0}", displayName));
                }
                // Were able to find a converter for the type but conversion failed.
                // Tell the model binding system to skip other model binders i.e. return non-null.
                return new ModelBindingResult(model: null, key: bindingContext.ModelName, isModelSet: false);
            }

            return null;
        }
    }
}
