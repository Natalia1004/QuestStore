using System;
using QuestStoreNAT.web.Models;
using Npgsql;
namespace QuestStoreNAT.web.DatabaseLayer
{
    public class GroupTransactionDAO : DBAbstractRecord<GroupTransaction>
    {
        public override string DBTableName { get; set; } = "GroupChosenArtifacts";
        private enum GroupTransactionEnum
        {
            Id, artifactID, groupID, numberOfStudent, numberOfAcceptance
        }

        public override GroupTransaction ProvideOneRecord(NpgsqlDataReader reader)
        {
            var groupTransaction = new GroupTransaction();
            groupTransaction.ID = reader.GetInt32((int)GroupTransactionEnum.Id);
            groupTransaction.artifactID = reader.GetInt32((int)GroupTransactionEnum.artifactID);
            groupTransaction.groupID = reader.GetInt32((int)GroupTransactionEnum.groupID);
            groupTransaction.numberOfStudents = reader.GetInt32((int)GroupTransactionEnum.numberOfStudent);
            groupTransaction.numberOfAcceptance = reader.GetInt32((int)GroupTransactionEnum.numberOfAcceptance);
            return groupTransaction;
        }

        public override string ProvideQueryStringToAdd(GroupTransaction groupTransactionToAdd)
        {
            var query = $"INSERT INTO \"NATQuest\".\"{DBTableName}\" (\"ID\", \"ArtifactID\", \"GroupID\", \"NumberOfStudents\", \"NumberOfAcceptances\")" +
                        $"VALUES({(int)groupTransactionToAdd.ID}, " +
                               $"'{groupTransactionToAdd.artifactID}', " +
                               $"'{groupTransactionToAdd.groupID}', " +
                               $"'{groupTransactionToAdd.numberOfStudents}', " +
                               $"'{groupTransactionToAdd.numberOfAcceptance}');";
            return query;
        }

        public override string ProvideQueryStringToUpdate(GroupTransaction groupTransactionToUpdate)
        {
            var query = $"UPDATE \"NATQuest\".\"{DBTableName}\" " +
                        $"SET \"ArtifactID\" = {(int)groupTransactionToUpdate.artifactID}, " +
                            $"\"GroupID\" = '{groupTransactionToUpdate.groupID}', " +
                            $"\"NumberOfStudents\" = '{groupTransactionToUpdate.numberOfStudents}', " +
                            $"\"NumberOfAcceptance\" = '{groupTransactionToUpdate.numberOfAcceptance}', " +
                        $"WHERE \"NATQuest\".\"{DBTableName}\".\"ID\" = {groupTransactionToUpdate.ID};";
            return query;
        }
    }
}
