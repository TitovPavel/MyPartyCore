using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using MyPartyCore.Models;

namespace MyPartyCore.DAL
{
    public class ADOParticipantsRepository : IParticipantsRepository
    {
        private string _connectionString;
        public ADOParticipantsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Participant participant)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sqlExpression = "INSERT INTO Participants (name, attend, reason, arrivalDate, partyId, userId) VALUES (@name, @attend, @reason, @arrivalDate, @partyId, 1)";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.AddWithValue("@name", participant.Name);
                command.Parameters.AddWithValue("@attend", participant.Attend);
                command.Parameters.AddWithValue("@reason", participant.Reason);
                command.Parameters.AddWithValue("@arrivalDate", participant.ArrivalDate);
                command.Parameters.AddWithValue("@partyId", participant.PartyId);
                int number = command.ExecuteNonQuery();
            }
        }

        public void Delete(Participant participant)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sqlExpression = "DELETE  FROM Participants WHERE id=@id";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.AddWithValue("@id", participant.Id);
                int number = command.ExecuteNonQuery();
            }
        }

        public List<Participant> GetAll()
        {
            List<Participant> participantList = new List<Participant>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM Participants";
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) 
                {
                    while (reader.Read())
                    {
                        participantList.Add(new Participant()
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Name = reader["name"]?.ToString(),
                            Attend = Convert.ToBoolean(reader["attend"]),
                            Reason = reader["reason"]?.ToString(),
                            ArrivalDate = Convert.ToDateTime(reader["arrivalDate"]),
                            PartyId = Convert.ToInt32(reader["partyId"]),
                            UserId = Convert.ToInt32(reader["userId"])
                        });
                    }
                }
                reader.Close();
            }

            return participantList;
        }

        public Participant GetById(int participantID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM Participants WHERE id = @id";
                command.Parameters.AddWithValue("@id", participantID);
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        return new Participant()
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Name = reader["name"]?.ToString(),
                            Attend = Convert.ToBoolean(reader["attend"]),
                            Reason = reader["reason"]?.ToString(),
                            ArrivalDate = Convert.ToDateTime(reader["arrivalDate"]),
                            PartyId = Convert.ToInt32(reader["partyId"]),
                            UserId = Convert.ToInt32(reader["userId"])
                        };
                    }
                }
                reader.Close();
            }

            return null;
        }

        public void Update(Participant participant)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sqlExpression = "UPDATE Participants SET name=@name, attend=@attend, reason=@reason, arrivalDate=@arrivalDate, partyId=@partyId, userId=@userId WHERE name=@name";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.AddWithValue("@id", participant.Id);
                command.Parameters.AddWithValue("@name", participant.Name);
                command.Parameters.AddWithValue("@attend", participant.Attend);
                command.Parameters.AddWithValue("@reason", participant.Reason);
                command.Parameters.AddWithValue("@arrivalDate", participant.ArrivalDate);
                command.Parameters.AddWithValue("@partyId", participant.PartyId);
                command.Parameters.AddWithValue("@userId", participant.UserId);

                int number = command.ExecuteNonQuery();
            }
        }
    }
}
