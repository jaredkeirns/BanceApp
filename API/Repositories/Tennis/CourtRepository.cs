﻿using API.DataAccess;
using API.Models.Tennis;
using API.Services.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace API.Repositories.Tennis
{
    public class CourtRepository : ICourtRepository
    {
        private string connectionString;

        public CourtRepository(IApplicationConfiguration config)
        {
            connectionString = config.ConnectionString;
        }

        public int SaveCourt(Court court)
        {
            var courtRecnumInDb = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SaveCourt", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@Name", SqlDbType.VarChar).Value = court.Name;
                command.Parameters.Add("@Lights", SqlDbType.Bit).Value = court.Lights;
                command.Parameters.Add("@Surface", SqlDbType.VarChar).Value = court.Surface;
                command.Parameters.Add("@Condition", SqlDbType.Int).Value = court.Condition;
                command.Parameters.Add("@LocationRecnum", SqlDbType.Int).Value = court.LocationRecnum;
                command.Connection.Open();
                using (EnhancedSqlDataReader reader = new EnhancedSqlDataReader(command.ExecuteReader()))
                {
                    while (reader.Read())
                    {
                        courtRecnumInDb = reader.GetInt32("recnum");
                    }
                }
                return courtRecnumInDb;
            }
        }

        public void AddCourtToCourtConditionTable(int courtRecnum, int ratingTotal, int numberOfRatings)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("AddCourtToCourtCondition", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@CourtRecnum", SqlDbType.Int).Value = courtRecnum;
                    command.Parameters.Add("@RatingTotal", SqlDbType.Int).Value = ratingTotal;
                    command.Parameters.Add("@NumberOfRatings", SqlDbType.Int).Value = numberOfRatings;
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
