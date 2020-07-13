using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
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
            };
        }

        public override void UpdateRecord(OwnedQuestStudent ownedQuestToUpdate)
        {
            using NpgsqlConnection connection = OpenConnectionToDB();
            string query = ProvideQueryStringToUpdate(ownedQuestToUpdate);
            ExecuteQuery(connection, query);
        }

        public override string ProvideQueryStringToUpdate(OwnedQuestStudent ownedQuestToUpdate)
        {
            var query = $"UPDATE \"NATQuest\".\"{DBTableName}\" " +
                        $"SET \"QuestStatusID\" = {ownedQuestToUpdate.CompletionStatus}"+
                        $"WHERE \"ID\" = {ownedQuestToUpdate.Id};";
            return query;
        }

        
        public override string ProvideQueryStringToAdd(OwnedQuestStudent recordToAdd)
        {
            throw new NotImplementedException();
        }
    }
}
