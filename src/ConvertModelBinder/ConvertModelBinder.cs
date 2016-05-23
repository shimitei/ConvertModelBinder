using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Internal;

namespace Microsoft.AspNetCore.Mvc.ModelBinding.Binders
{
    public class ConvertModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueProviderResult == ValueProviderResult.None)
            {
                // no entry
                bindingContext.Result = ModelBindingResult.Failed(bindingContext.ModelName);
                return TaskCache.CompletedTask;
            }

            bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

            try
            {
                if (string.IsNullOrEmpty(valueProviderResult.Values))
                {
                    if (bindingContext.ModelType == typeof(decimal?))
                    {
                        decimal? defaultValue = null;
                        bindingContext.Result = ModelBindingResult.Success(bindingContext.ModelName, defaultValue);
                        return TaskCache.CompletedTask;
                    }
                    else
                    {
                        decimal defaultValue = 0.0M;
                        bindingContext.Result = ModelBindingResult.Success(bindingContext.ModelName, defaultValue);
                        return TaskCache.CompletedTask;
                    }
                }

                decimal model = Convert.ToDecimal(
                        valueProviderResult.Values,
                        CultureInfo.InvariantCulture);

                bindingContext.Result = ModelBindingResult.Success(bindingContext.ModelName, model);
                return TaskCache.CompletedTask;
            }
            catch (Exception exception)
            {
                bindingContext.ModelState.TryAddModelError(
                    bindingContext.ModelName,
                    exception,
                    bindingContext.ModelMetadata);

                //customized error message
                //string displayName = bindingContext.ModelMetadata.DisplayName ?? bindingContext.ModelName;
                //bindingContext.ModelState.TryAddModelError(bindingContext.ModelName,
                //    string.Format("not decimal input:{0}", displayName));

                // Were able to find a converter for the type but conversion failed.
                // Tell the model binding system to skip other model binders.
                bindingContext.Result = ModelBindingResult.Failed(bindingContext.ModelName);
                return TaskCache.CompletedTask;
            }
        }
    }
}
