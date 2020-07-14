﻿using Npgsql;
using QuestStoreNAT.web.Models;

namespace QuestStoreNAT.web.DatabaseLayer
{
    public class OwnedQuestStudentDAO : DBAbstractRecord<OwnedQuestStudent>
    {
        public override string DBTableName { get; set; } = "OwnedQuestStudent";

        private enum OwnedQuestStudentEnum
        {
            Id, StudentId, QuestId, QuestStatusId
        }

        public override OwnedQuestStudent ProvideOneRecord(NpgsqlDataReader reader)
        {
            return new OwnedQuestStudent
            {
                Id = reader.GetInt32((int)OwnedQuestStudentEnum.Id),
                StudentId = reader.GetInt32((int)OwnedQuestStudentEnum.StudentId),
                QuestId = reader.GetInt32((int)OwnedQuestStudentEnum.QuestId),
                CompletionStatus = reader.GetInt32((int)OwnedQuestStudentEnum.QuestStatusId)
                //Why not Enum
                //CompletionStatus = (CompletionStatus)reader.GetInt32((int)OwnedQuestStudentEnum.QuestStatusId)
            };
        }

        //TODO DELETE IF NO ERRORS UNTIL 17.07
        //public override void UpdateRecord(OwnedQuestStudent ownedQuestToUpdate)
        //{
        //    using NpgsqlConnection connection = OpenConnectionToDB();
        //    string query = ProvideQueryStringToUpdate(ownedQuestToUpdate);
        //    ExecuteQuery(connection, query);
        //}

        public override string ProvideQueryStringToAdd(OwnedQuestStudent recordToAdd)
        {
            var query = $"INSERT INTO \"NATQuest\".\"{DBTableName}\" (\"StudentID\", \"QuestID\", \"QuestStatusID\")" +
                        $"VALUES({recordToAdd.StudentId}, " +
                        $"{recordToAdd.QuestId}, " +
                        $"{(int)recordToAdd.CompletionStatus});";
            return query;
        }

        public override string ProvideQueryStringToUpdate(OwnedQuestStudent ownedQuestToUpdate)
        {
            var query = $"UPDATE \"NATQuest\".\"{DBTableName}\" " +
                        $"SET \"QuestStatusID\" = {ownedQuestToUpdate.CompletionStatus}"+
                        $"WHERE \"ID\" = {ownedQuestToUpdate.Id};";
            return query;
        }
    }
}