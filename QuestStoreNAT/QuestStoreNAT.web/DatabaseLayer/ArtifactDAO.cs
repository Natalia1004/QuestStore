using System;
using QuestStoreNAT.web.Models;
using System.Collections.Generic;
using Npgsql;

namespace QuestStoreNAT.web.DatabaseLayer
{
    public class ArtifactDAO : DBAbstractRecord<Artifact>
    {
        public override string DBTableName { get; set; } = "Artifacts";
        private enum ArtifactEnum
        {
            Id, ArtifactTypeID, Name, Cost, Description
        }

        public override Artifact ProvideOneRecord(NpgsqlDataReader reader)
        {
            var artifact = new Artifact();
            artifact.Id = reader.GetInt32((int)ArtifactEnum.Id);
            artifact.Name = reader.GetString((int)ArtifactEnum.Name);
            artifact.Cost = reader.GetInt32((int)ArtifactEnum.Cost);
            artifact.Description = reader.GetString((int)ArtifactEnum.Description);
            artifact.Type = (TypeClassification)reader.GetInt32((int)ArtifactEnum.ArtifactTypeID);
            return artifact;
        }

        public override string ProvideQueryStringToAdd(Artifact artifactToAdd)
        {
            var query = $"INSERT INTO \"NATQuest\".\"{DBTableName}\" (\"ArtifactTypeID\", \"Name\", \"Cost\", \"Description\")" +
                        $"VALUES({(int)artifactToAdd.Type}, " +
                               $"'{artifactToAdd.Name}', " +
                               $"{artifactToAdd.Cost}, " +
                               $"'{artifactToAdd.Description}');";
            return query;
        }

        public override string ProvideQueryStringToUpdate(Artifact artifactToUpdate)
        {
            var query = $"UPDATE \"NATQuest\".\"{DBTableName}\" " +
                        $"SET \"ArtifactTypeID\" = {(int)artifactToUpdate.Type}, " +
                            $"\"Name\" = '{artifactToUpdate.Name}', " +
                            $"\"Cost\" = '{artifactToUpdate.Cost}', " +
                            $"\"Description\" = '{artifactToUpdate.Description}'" +
                        $"WHERE \"NATQuest\".\"{DBTableName}\".\"Id\" = {artifactToUpdate.Id};";
            return query;
        }
    }
}
