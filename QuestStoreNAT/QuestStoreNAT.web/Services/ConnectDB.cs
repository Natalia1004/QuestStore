using System;
using Npgsql;

namespace QuestStoreNAT.web.Services
{
    public class ConnectDB
    {
        public static void ExecuteNonQuery(string sql)
        {
            using NpgsqlConnection connection = CreateNewConnection();
            connection.Open();

            using NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            command.ExecuteNonQuery();
        }

        public static NpgsqlConnection CreateNewConnection()
        {
            string accessConnection = "Host=localhost;Username=nataliafilipek;Password=postgres;Database=students";
            NpgsqlConnection connection = new NpgsqlConnection(accessConnection);
            return connection;

        }
    }
}
