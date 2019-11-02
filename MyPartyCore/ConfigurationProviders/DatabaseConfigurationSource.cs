using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPartyCore.ConfigurationProviders
{
    public class DatabaseConfigurationSource : IConfigurationSource
    {
        private readonly string _connectionString;
        
        public DatabaseConfigurationSource(string connectionString)
        {
            _connectionString = connectionString;
        }
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new DatabaseConfigurationProvider(_connectionString);
        }
    }
}
