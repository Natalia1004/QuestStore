using Npgsql;

namespace QuestStoreNAT.web.DatabaseLayer
{
    public class ConnectDB
    {
        private const string Host = "kandula.db.elephantsql.com";
        private const string Username = "jvdwmero";
        private const string Password = "GSy9rkphAxYyU__75_leDG1iIFQFQMVe";
        private const string Database = "jvdwmero";

        public static string GetConnectionString()
        {
            return $"Host={Host};Username={Username};Password={Password};Database={Database}";
        }
        
        public static void ExecuteNonQuery(string sql)
        {
            using NpgsqlConnection connection = CreateNewConnection();
            connection.Open();

            using NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            command.ExecuteNonQuery();
        }

        public static NpgsqlConnection CreateNewConnection()
        {
            string accessConnection = GetConnectionString();
            NpgsqlConnection connection = new NpgsqlConnection(accessConnection);
            return connection;
        }
    }
}
