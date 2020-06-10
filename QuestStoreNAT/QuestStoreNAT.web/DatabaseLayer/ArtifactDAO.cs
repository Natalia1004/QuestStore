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
                    ArtifactID = Convert.ToInt16(reader[0]),
                    ArtifactStatusID = Convert.ToInt16(reader[1]),
                    ArtifactTypeID = Convert.ToInt16(reader[2]),
                    Name = Convert.ToString(reader[3]),
                    Cost = Convert.ToInt16(reader[4]),
                    Description = Convert.ToString(reader[5]),
                };
                dataArtifacts.Add(artifact);
            }
            return dataArtifacts;
        }
    }
}
