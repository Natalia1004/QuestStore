using System;
using QuestStoreNAT.web.Models;
using Npgsql;

namespace QuestStoreNAT.web.DatabaseLayer
{
    public class CredentialsDAO
    {
        private const string dbTableName = "Credentials";
        private enum CredentialsEnum
        {
            Id, Role, Email, Password
        }

        public Credentials FindCredentials(string email)
        {
            if (String.IsNullOrEmpty(email)) throw new ArgumentNullException(nameof(email));

            using NpgsqlConnection connection = ConnectDB.CreateNewConnection();
            connection.Open();
            var query = $"SELECT * FROM \"NATQuest\".\"{dbTableName}\" WHERE \"NATQuest\".\"{dbTableName}\".\"Email\" = '{email}' LIMIT 1;";
            using var command = new NpgsqlCommand(query, connection);
            var reader = command.ExecuteReader();

            Credentials foundCredentials = null;
            while (reader.Read())
            {
                foundCredentials = ProvideOneRecord(reader);
            };
            return foundCredentials;
        }

        private Credentials ProvideOneRecord(NpgsqlDataReader reader)
        {
            var credentials = new Credentials();
            credentials.Id = reader.GetInt32((int)CredentialsEnum.Id);
            credentials.Role = (Role)reader.GetInt32((int)CredentialsEnum.Role);
            credentials.Email = reader.GetString((int)CredentialsEnum.Email);
            credentials.Password = reader.GetString((int)CredentialsEnum.Password);
            return credentials;
        }
    }
}
