using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace Microsoft.AspNet.Mvc.ModelBinding
{
    public static class ConvertModelBinderExtensions
    {
        public static MvcOptions AddConvertModelBinder(this MvcOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            options.ModelBinders.Insert(0, new ConvertModelBinder());
            return options;
        }
    }
}
