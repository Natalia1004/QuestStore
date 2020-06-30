using System;
using Npgsql;
using QuestStoreNAT.web.Models;
using System.Collections.Generic;

namespace QuestStoreNAT.web.DatabaseLayer
{
    public class GroupDAO : DBAbstractRecord<Group>
    {
        public override string DBTableName { get; set; } = "Groups";

        private enum GroupEnum
        {
            Id, ClassId, Name, CoinsTotal
        }

        public override Group ProvideOneRecord(NpgsqlDataReader reader)
        {
            var group = new Group();
            group.Id = reader.GetInt32((int)GroupEnum.Id);
            group.ClassroomId = reader.GetInt32((int)GroupEnum.ClassId);
            group.Name = reader.GetString((int)GroupEnum.Name);
            group.GroupWallet = reader.GetInt32((int)GroupEnum.CoinsTotal);
            return group;
        }
        public override string ProvideQueryStringToAdd(Group GroupToAdd)
        {
            var query = $"INSERT INTO \"NATQuest\".\"{DBTableName}\" (\"ClassID\", \"Name\", \"CoinsTotal\")" +
                        $"VALUES({(int)GroupToAdd.ClassroomId}, " +
                               $"'{GroupToAdd.Name}', " +
                               $"'{GroupToAdd.GroupWallet}');";
            return query;
        }

        public override string ProvideQueryStringToUpdate(Group GroupToUpdate)
        {
            var query = $"UPDATE \"NATQuest\".\"{DBTableName}\" " +
                        $"SET \"ClassID\" = {GroupToUpdate.ClassroomId}, " +
                            $"\"Name\" = '{GroupToUpdate.Name}', " +
                            $"\"CoinsTotal\" = '{GroupToUpdate.GroupWallet}' " +
                        $"WHERE \"NATQuest\".\"{DBTableName}\".\"ID\" = {GroupToUpdate.Id};";
            return query;
        }
        
    }
}
