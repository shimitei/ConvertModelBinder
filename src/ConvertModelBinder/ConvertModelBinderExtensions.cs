using System;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace Microsoft.AspNetCore.Mvc
{
    public static class ConvertModelBinderExtensions
    {
        public static MvcOptions AddConvertModelBinder(this MvcOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            options.ModelBinderProviders.Insert(0, new ConvertModelBinderProvider());
            return options;
        }
    }
}
