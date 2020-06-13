using Npgsql;
using QuestStoreNAT.web.Models;
using System;

namespace QuestStoreNAT.web.DatabaseLayer
{
    public class AdminDAO
    {
        private const string dbTableName = "Admins";
        private enum AdminsEnum
        {
            Id, FirstName, LastName
        }

        public Admin FindAdmin(string email)
        {
            if (String.IsNullOrEmpty(email)) throw new ArgumentNullException(nameof(email));

            using NpgsqlConnection connection = ConnectDB.CreateNewConnection();
            connection.Open();
            var query = $"SELECT * FROM \"NATQuest\".\"{dbTableName}\" WHERE \"NATQuest\".\"{dbTableName}\".\"Email\" = '{email}' LIMIT 1;";
            using var command = new NpgsqlCommand(query, connection);
            var reader = command.ExecuteReader();

            Admin foundAdmin = null;
            while (reader.Read())
            {
                foundAdmin = ProvideOneRecord(reader, email);
            };
            return foundAdmin;
        }

        private Admin ProvideOneRecord(NpgsqlDataReader reader, string email)
        {
            var credentialsDAO = new CredentialsDAO();

            var admin = new Admin();
            admin.Id = reader.GetInt32((int)AdminsEnum.Id);
            admin.FirstName = reader.GetString((int)AdminsEnum.FirstName);
            admin.LastName = reader.GetString((int)AdminsEnum.LastName);
            admin.Credentials = credentialsDAO.FindCredentials(email);
            return admin;
        }
    }
}
