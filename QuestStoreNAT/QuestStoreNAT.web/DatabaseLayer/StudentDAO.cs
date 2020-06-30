using System;
using Npgsql;
using QuestStoreNAT.web.Models;
using System.Collections.Generic;

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
            student.ClassID = reader.GetInt32((int)StudentEnum.ClassId);
            student.GroupID = reader.GetInt32((int)StudentEnum.GroupId);
            student.FirstName = reader.GetString((int)StudentEnum.FirstName);
            student.LastName = reader.GetString((int)StudentEnum.Surname);
            student.Wallet = reader.GetInt32((int)StudentEnum.CoinsTotal);
            student.OverallWalletLevel = reader.GetInt32((int)StudentEnum.CoinsBalance);
            student.CredentialID = reader.GetInt16((int)StudentEnum.CredentialID);
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
            var query = $"UPDATE \"NATQuest\".\"{DBTableName}\" " +
                        $"SET \"ClassID\" = {studentToUpdate.ClassID}," + 
                        $"\"GroupID\" = {studentToUpdate.GroupID}," +
                        $"\"FirstName\" = '{studentToUpdate.FirstName}', " +
                        $"\"Surname\" = '{studentToUpdate.LastName}'," +
                        $"\"CoinsTotal\" = {studentToUpdate.Wallet}," +
                        $"\"CoinsBalance\" = {studentToUpdate.OverallWalletLevel}" +
                        $"WHERE \"ID\" = {studentToUpdate.Id};";
            return query;
        }

        public override void UpdateRecord( Student studentToUpdate )
        {
            using NpgsqlConnection connection = OpenConnectionToDB();
            string query = ProvideQueryStringToUpdate(studentToUpdate);
            ExecuteQuery(connection , query);
        }

        #region outOfAbstraction
        public string ProvideQueryStringReturningID( Student studentToAdd )
        {
            var query = $"INSERT INTO \"NATQuest\".\"{DBTableName}\" " +
                        $"(\"ClassID\", \"GroupID\", \"Credential_ID\", \"FirstName\", \"Surname\", \"CoinsBalance\", \"CoinsTotal\")" +
                        $"VALUES({studentToAdd.ClassID}, {studentToAdd.GroupID}, {studentToAdd.CredentialID}," +
                              $"'{studentToAdd.FirstName}','{studentToAdd.LastName}'," +
                              $"{studentToAdd.Wallet}," +
                              $"{studentToAdd.OverallWalletLevel}) RETURNING \"ID\";";
            return query;
        }

        public int AddStudentByCredentialsReturningID( int credentialID ) // credentialID from CredentialsDAO.AddRecordReturningID(Credentials newCredential)
        {
            var newStudent = new Student
            {
                ClassID = 200,
                GroupID = 200,
                CredentialID = credentialID,
                FirstName = "" ,
                LastName = "" ,
                Wallet = 0 ,
                OverallWalletLevel = 0
            };

            using NpgsqlConnection connection = OpenConnectionToDB();
            string query = ProvideQueryStringReturningID(newStudent);
            return ExecuteScalar(connection , query); // StudentID used to instanly update Student
        }

        private int ExecuteScalar( NpgsqlConnection connection , string query )
        {
            using var command = new NpgsqlCommand(query , connection);
            command.Prepare();
            return Convert.ToInt32(command.ExecuteScalar());
        }
        #endregion
    }
}
