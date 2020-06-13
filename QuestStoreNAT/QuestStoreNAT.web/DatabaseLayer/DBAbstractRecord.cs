using Npgsql;

namespace QuestStoreNAT.web.DatabaseLayer
{
    public abstract class DBAbstractRecord<T>
    {
        public abstract string DBTableName { get; set; }
        public abstract T ProvideOneRecord(NpgsqlDataReader reader);

        public virtual T FindOneRecordBy(string email)
        {
            using NpgsqlConnection connection = OpenConnectionToDB();
            var query = $"SELECT * FROM \"NATQuest\".\"{DBTableName}\" WHERE \"NATQuest\".\"{DBTableName}\".\"Email\" = '{email}' LIMIT 1;";
            using var command = new NpgsqlCommand(query, connection);
            var reader = command.ExecuteReader();

            var oneRecord = default(T);
            while (reader.Read())
            {
                oneRecord = ProvideOneRecord(reader);
            };
            return oneRecord;
        }

        protected NpgsqlConnection OpenConnectionToDB()
        {
            var connection = new NpgsqlConnection(ConnectDB.GetConnectionString());
            connection.Open();
            return connection;
        }

        protected void ExecuteQuery(NpgsqlConnection connection, string query)
        {
            using var command = new NpgsqlCommand(query, connection);
            command.Prepare();
            command.ExecuteNonQuery();
        }
    }
}
