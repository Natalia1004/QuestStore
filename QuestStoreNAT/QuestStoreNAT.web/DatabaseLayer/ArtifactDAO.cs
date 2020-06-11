using System;
using QuestStoreNAT.web.Models;
using System.Collections.Generic;
using Npgsql;

namespace QuestStoreNAT.web.DatabaseLayer
{
    public class ArtifactDAO : IArtifactDAO
    { 
        public List<Artifact> GetAllRows()
        {
            List<Artifact> dataArtifacts = new List<Artifact>();

            using NpgsqlConnection connection = ConnectDB.CreateNewConnection();
            connection.Open();

            string sql = $"SELECT * FROM \"NATQuest\".\"Artifacts\" ";
            using NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            using NpgsqlDataReader reader = command.ExecuteReader();

            int countOfData = reader.FieldCount;

            while (reader.Read())
            {
                Artifact artifact = new Artifact()
                {
                    Id = Convert.ToInt16(reader[0]),
                    Type = (TypeClassification)Convert.ToInt16(reader[1]),
                    Name = Convert.ToString(reader[2]),
                    Cost = Convert.ToInt16(reader[3]),
                    Description = Convert.ToString(reader[4]),
                };
                dataArtifacts.Add(artifact);
            }
            return dataArtifacts;
        }

        public static void InsertRow(string tableName, string[] values)
        {
            string sql = $"INSERT INTO {tableName} VALUES ({string.Join(',', values)})";
            ConnectDB.ExecuteNonQuery(sql);
        }
    }
}