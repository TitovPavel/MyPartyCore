using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPartyCore.ConfigurationProviders
{
    public static class DatabaseConfigurationExtensions
    {
        public static IConfigurationBuilder AddDatabaseConfiguration(this IConfigurationBuilder builder, string connectionString)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException("Путь к файлу не указан");
            }

            var source = new DatabaseConfigurationSource(connectionString);
            builder.Add(source);
            return builder;
        }
    }
}
