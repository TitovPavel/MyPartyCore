using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MyPartyCore.ConfigurationProviders
{
    public class DatabaseConfigurationProvider : ConfigurationProvider
    {
        private readonly string _connectionString;

        public DatabaseConfigurationProvider(string connectionString)
        {
            _connectionString = connectionString;
        }
        public override void Load()
        {
            var data = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM Settings";
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string key = reader["key"]?.ToString();
                        string value = reader["value"]?.ToString();
                        data.Add(key, value);
                    }
                }
                reader.Close();
            }

            Data = data;
        }
    }
}
