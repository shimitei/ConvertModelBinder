using System;

namespace Microsoft.AspNetCore.Mvc.ModelBinding.Binders
{
    public class ConvertModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (!context.Metadata.IsComplexType)
            {
                Type type = context.Metadata.ModelType;
                if (type == typeof(decimal) || type == typeof(decimal?))
                {
                    return new ConvertModelBinder();
                }
            }

            return null;
        }
    }
}
