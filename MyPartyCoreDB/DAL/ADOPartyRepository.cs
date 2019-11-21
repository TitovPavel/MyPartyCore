using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using MyPartyCore.DB.Models;

namespace MyPartyCore.DB.DAL
{
    public class ADOPartyRepository : IPartyRepository
    {
        private string _connectionString;
        public ADOPartyRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Party party)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sqlExpression = "INSERT INTO Parties (title, location, date, ownerId) VALUES (@title, @location, @date, @ownerId)";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.AddWithValue("@title", party.Title);
                command.Parameters.AddWithValue("@location", party.Location);
                command.Parameters.AddWithValue("@date", party.Date);
                command.Parameters.AddWithValue("@ownerId", party.Owner.Id);
                int number = command.ExecuteNonQuery();
            }
        }

        public void Delete(Party party)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sqlExpression = "DELETE  FROM Parties WHERE id=@id";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.AddWithValue("@id", party.Id);
                int number = command.ExecuteNonQuery();
            }
        }

        public List<Party> GetAll()
        {
            List<Party> partyList = new List<Party>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM Parties";
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) 
                {
                    while (reader.Read())
                    {
                        partyList.Add(new Party()
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Title = reader["title"]?.ToString(),
                            Date = Convert.ToDateTime(reader["date"]),
                            Location = reader["location"]?.ToString()
                        });
                    }
                }
                reader.Close();
            }

            return partyList;
        }

        public Party GetById(int partyID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM Parties WHERE id = @id";
                command.Parameters.AddWithValue("@id", partyID);
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) 
                {
                    while (reader.Read())
                    {
                        return new Party()
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Title = reader["title"]?.ToString(),
                            Date = Convert.ToDateTime(reader["date"]),
                            Location = reader["location"]?.ToString()
                        };
                    }
                }
                reader.Close();
            }

            return null;
        }

        public void Update(Party party)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sqlExpression = "UPDATE Users SET title=@title, location=@location, date=@date, ownerId=@ownerId WHERE id = @id";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.AddWithValue("@id", party.Id);
                command.Parameters.AddWithValue("@title", party.Title);
                command.Parameters.AddWithValue("@location", party.Location);
                command.Parameters.AddWithValue("@date", party.Date);
                command.Parameters.AddWithValue("@ownerId", party.Owner.Id);

                int number = command.ExecuteNonQuery();
            }
        }

        public List<Party> GetNearestParties(int limit)
        {
            List<Party> partyList = new List<Party>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT TOP (@limit) * FROM Parties WHERE [date] > CURRENT_TIMESTAMP Order By date";
                command.Parameters.AddWithValue("@limit", limit);
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        partyList.Add(new Party()
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Title = reader["title"]?.ToString(),
                            Date = Convert.ToDateTime(reader["date"]),
                            Location = reader["location"]?.ToString()
                        });
                    }
                }
                reader.Close();
            }

            return partyList;
        }

        public List<Party> GetFututeParties()
        {
            List<Party> partyList = new List<Party>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM Parties WHERE [date] > CURRENT_TIMESTAMP Order By date";
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        partyList.Add(new Party()
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Title = reader["title"]?.ToString(),
                            Date = Convert.ToDateTime(reader["date"]),
                            Location = reader["location"]?.ToString()
                        });
                    }
                }
                reader.Close();
            }

            return partyList;
        }
    }
}
