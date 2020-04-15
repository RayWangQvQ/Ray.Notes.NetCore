using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Ray.EssayNotes.DDD.DbConfigurationDemo;

namespace Microsoft.Extensions.Configuration
{
    public static class DbConfigurationExtensions
    {
        public static IConfigurationBuilder AddDatabase(this IConfigurationBuilder builder, string connectionStringName, IDictionary<string, string> initialSettings = null)
        {
            var connectionString = builder.Build()
                .GetConnectionString(connectionStringName);

            var source = new DbConfigurationSource(optionsBuilder => optionsBuilder.UseSqlServer(connectionString), initialSettings);
            builder.Add(source);
            return builder;
        }
    }
}
