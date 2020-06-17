using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
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


        public override string ProvideQueryStringToAdd(Student studentToAdd)
        {
            throw new System.NotImplementedException();
        }


        public override string ProvideQueryStringToUpdate( Student studentToUpdate )
        {
            var query = $"UPDATE \"NATQuest\".\"{DBTableName}\" " +
                        $"SET (\"ClassID\" = {studentToUpdate.ClassID}," + 
                        $"\"GroupID\" = {studentToUpdate.GroupID}," +
                        $"\"FirstName\" = {studentToUpdate.FirstName}, " +
                        $"\"Surname\" = {studentToUpdate.LastName}," +
                        $"\"CoinsBalance\" = {studentToUpdate.Wallet}," +
                        $"\"CoinsTotal\" = {studentToUpdate.OverallWalletLevel})" +
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
                        $"(\"ClassID\", \"GroupID\", \"CredentialID\", \"FirstName\", \"Surname\", \"CoinsBalance\", \"CoinsTotal\")" +
                        $"VALUES({studentToAdd.ClassID}, {studentToAdd.GroupID}, {studentToAdd.CredentialID}," +
                              $"'{studentToAdd.FirstName}','{studentToAdd.LastName}'," +
                              $"{studentToAdd.Wallet}," +
                              $"{studentToAdd.OverallWalletLevel}) RETURNING ID;";
            return query;
        }


        public int AddStudentByCredentialsReturningID( int credentialID ) // credentialID from CredentialsDAO.AddRecordReturningID(Credentials newCredential)
        {
            var newStudent = new Student
            {
                ClassID = -1,
                GroupID = -1,
                CredentialID = credentialID,
                FirstName = "" ,
                LastName = "" ,
                Wallet = 0 ,
                OverallWalletLevel = 0
            };

            using NpgsqlConnection connection = OpenConnectionToDB();
            string query = ProvideQueryStringToAdd(newStudent);
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
