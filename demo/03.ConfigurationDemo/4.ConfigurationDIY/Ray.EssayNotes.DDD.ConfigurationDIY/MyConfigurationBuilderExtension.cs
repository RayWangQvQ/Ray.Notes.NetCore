using System;
using System.Collections.Generic;
using System.Text;
using Ray.EssayNotes.DDD.ConfigurationDIY;

namespace Microsoft.Extensions.Configuration
{
    public static class MyConfigurationBuilderExtension
    {
        public static IConfigurationBuilder AddMyConfiguration(this IConfigurationBuilder builder)
        {
            builder.Add(new MyConfigurationSource());
            return builder;
        }
    }
}
