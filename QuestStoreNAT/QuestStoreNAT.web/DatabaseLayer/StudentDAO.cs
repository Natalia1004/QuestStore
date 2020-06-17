using Npgsql;
using QuestStoreNAT.web.Models;

namespace QuestStoreNAT.web.DatabaseLayer
{
    public class StudentDAO : DBAbstractRecord<Student>
    {
        public override string DBTableName { get; set; } = "Students";
        private enum StudentEnum
        {
            Id, ClassId, GroupId, Email, Password, FirstName, Surname, CoinsTotal, CoinsBalance, CredentialID
        }

        public override Student ProvideOneRecord(NpgsqlDataReader reader)
        {
            var student = new Student();
            student.Id = reader.GetInt32((int)StudentEnum.Id);
            student.FirstName = reader.GetString((int)StudentEnum.FirstName);
            student.LastName = reader.GetString((int)StudentEnum.Surname);
            student.Wallet = reader.GetInt32((int)StudentEnum.CoinsTotal);
            student.OverallWalletLevel = reader.GetInt32((int)StudentEnum.CoinsBalance);
            return student;
        }

        public override Student FindOneRecordBy(int id)
        {
            using NpgsqlConnection connection = OpenConnectionToDB();
            var query = $"SELECT * FROM \"NATQuest\".\"{DBTableName}\" WHERE \"Credential_ID\" = '{id}';";
            using var command = new NpgsqlCommand(query, connection);
            var reader = command.ExecuteReader();

            var oneRecord = default(Student);
            while (reader.Read())
            {
                oneRecord = ProvideOneRecord(reader);
            };
            return oneRecord;
        }

        public override string ProvideQueryStringToAdd(Student studentToAdd)
        {
            throw new System.NotImplementedException();
        }

        public override string ProvideQueryStringToUpdate(Student studentToUpdate)
        {
            throw new System.NotImplementedException();
        }
    }
}
