using Npgsql;
using QuestStoreNAT.web.Models;

namespace QuestStoreNAT.web.DatabaseLayer
{
    public class QuestDAO : DBAbstractRecord<Quest>
    {
        public override string DBTableName { get; set; } = "Quests";
        private enum QuestEnum
        {
            Id, QuestType, Name, Cost, Description
        }

        public override Quest ProvideOneRecord(NpgsqlDataReader reader)
        {
            var quest = new Quest();
            quest.Id = reader.GetInt32((int)QuestEnum.Id);
            quest.Name = reader.GetString((int)QuestEnum.Name);
            quest.Cost = reader.GetInt32((int)QuestEnum.Cost);
            quest.Description = reader.GetString((int)QuestEnum.Description);
            quest.Type = (TypeClassification)reader.GetInt32((int)QuestEnum.QuestType);
            return quest;
        }

        public override string ProvideQueryStringToAdd(Quest questToAdd)
        {
            var query = $"INSERT INTO \"NATQuest\".\"{DBTableName}\" (\"NATQuest\".\"{DBTableName}\".\"QuestID\", \"NATQuest\".\"{DBTableName}\".\"QuestTypeID\",\"NATQuest\".\"{DBTableName}\".\"Name\",\"NATQuest\".\"{DBTableName}\".\"Cost\",\"NATQuest\".\"{DBTableName}\".\"Description\")" +
                        $"VALUES({questToAdd.Id}, " +
                               $"{questToAdd.Type}, " +
                               $"'{questToAdd.Name}', " +
                               $"{questToAdd.Cost}, " +
                               $"'{questToAdd.Description}');";
            return query;
        }

        public override string ProvideQueryStringToUpdate(Quest questToUpdate)
        {
            var query = $"UPDATE \"NATQuest\".\"{DBTableName}\" " +
                        $"SET \"NATQuest\".\"{DBTableName}\".\"QuestTypeID\" = {questToUpdate.Type}, " +
                            $"\"NATQuest\".\"{DBTableName}\".\"Name\" = '{questToUpdate.Name}', " +
                            $"\"NATQuest\".\"{DBTableName}\".\"Cost\" = '{questToUpdate.Cost}', " +
                            $"\"NATQuest\".\"{DBTableName}\".\"Description\" = '{questToUpdate.Description}'" +
                        $"WHERE \"NATQuest\".\"{DBTableName}\".\"QuestID\" = {questToUpdate.Id};";
            return query;
        }
    }
}
