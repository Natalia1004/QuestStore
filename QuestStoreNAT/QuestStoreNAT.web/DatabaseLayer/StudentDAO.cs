using Npgsql;
using QuestStoreNAT.web.Models;

namespace QuestStoreNAT.web.DatabaseLayer
{
    public class StudentDAO : DBAbstractRecord<Student>
    {
        public override string DBTableName { get; set; } = "Students";
        private enum StudentEnum
        {
            Id, ClassId, GroupId, Email, Password, FirstName, LastName, Wallet, Level
        }

        public override Student ProvideOneRecord(NpgsqlDataReader reader)
        {
            var student = new Student();
            student.Id = reader.GetInt32((int)StudentEnum.Id);
            student.FirstName = reader.GetString((int)StudentEnum.FirstName);
            student.LastName = reader.GetString((int)StudentEnum.LastName);
            student.Wallet = reader.GetInt32((int)StudentEnum.Wallet);
            student.OverallWalletLevel = reader.GetInt32((int)StudentEnum.Level);
            //TODO Credential retrieval ?
            return student;
        }
    }
}
