using System;
using Npgsql;

namespace QuestStoreNAT.web.DatabaseLayer
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
            string accessConnection = "Host=kandula.db.elephantsql.com;Username=jvdwmero;Password=GSy9rkphAxYyU__75_leDG1iIFQFQMVe;Database=jvdwmero";
            NpgsqlConnection connection = new NpgsqlConnection(accessConnection);
            return connection;
        }
    }
}
