using Microsoft.Extensions.Configuration;
using Npgsql;

namespace QuestStoreNAT.web.DatabaseLayer
{
    public static class ConnectDB
    {
        public static IConfiguration Configuration;

        public static string GetConnectionString()
        {
            return Configuration.GetConnectionString("ElephantSQL");
        }

        //TODO DELETE IF NO ERRORS UNTIL 17.07
        //public static void ExecuteNonQuery(string sql)
        //{
        //    using NpgsqlConnection connection = CreateNewConnection();
        //    connection.Open();

        //    using NpgsqlCommand command = new NpgsqlCommand(sql, connection);
        //    command.ExecuteNonQuery();
        //}

        //public static NpgsqlConnection CreateNewConnection()
        //{
        //    string accessConnection = GetConnectionString();
        //    NpgsqlConnection connection = new NpgsqlConnection(accessConnection);
        //    return connection;
        //}
    }
}
