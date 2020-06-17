﻿using System;
using QuestStoreNAT.web.Models;
using Npgsql;
using Npgsql.PostgresTypes;

namespace QuestStoreNAT.web.DatabaseLayer
{
    public class CredentialsDAO : DBAbstractRecord2<Credentials>
    {
        public override string DBTableName { get; set; } = "Credentials";


        private enum CredentialsEnum
        {
            Id, Role, Email, Password
        }


        #region standardDAOimplementation
        public Credentials FindCredentials(string email)
        {
            if (String.IsNullOrEmpty(email)) throw new ArgumentNullException(nameof(email));

            using NpgsqlConnection connection = ConnectDB.CreateNewConnection();
            connection.Open();
            var query = $"SELECT * FROM \"NATQuest\".\"{DBTableName}\" WHERE \"NATQuest\".\"{DBTableName}\".\"Email\" = '{email}' LIMIT 1;";
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

        #endregion


        #region IdReturtingDAOimplementation

        public override string ProvideQueryStringToAdd( Credentials credentialToAdd )
        {
            var query = $"INSERT INTO \"NATQuest\".\"{DBTableName}\" (\"Role\", \"Email\", \"Password\")" +
                        $"VALUES({credentialToAdd.Role}, " +
                               $"'{credentialToAdd.Email}', " +
                               $"{credentialToAdd.Password}) RETURNING ID ";
            return query;
        }

        public override int AddRecordReturningID(Credentials newCredential)
        {
            using NpgsqlConnection connection = OpenConnectionToDB();
            string query = ProvideQueryStringToAdd(newCredential);
            return ExecuteScalar(connection , query);
        }

        #endregion
    }
}
