using System;
using QuestStoreNAT.web.Models;
using System.Collections.Generic;
using Npgsql;

namespace QuestStoreNAT.web.DatabaseLayer
{
    public class CredentialsDAO
    {
        private const string dbTableName = "Credentials";
        private enum CredentialsEnum
        {
            id, role, email, password
        }

        public Credentials FindCredentials(string email)
        {
            if (String.IsNullOrEmpty(email)) throw new ArgumentNullException(nameof(email));

            using NpgsqlConnection connection = ConnectDB.CreateNewConnection();
            connection.Open();
            var query = $"SELECT * FROM \"NATQuest\".\"{dbTableName}\" WHERE \"NATQuest\".\"Credentials\".\"Email\" = '{email}' LIMIT 1;";
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
            credentials.Id = reader.GetInt32((int)CredentialsEnum.id);
            credentials.Role = (Role)reader.GetInt32((int)CredentialsEnum.role);
            credentials.Email = reader.GetString((int)CredentialsEnum.email);
            credentials.Password = reader.GetString((int)CredentialsEnum.password);
            return credentials;
        }
    }
}
