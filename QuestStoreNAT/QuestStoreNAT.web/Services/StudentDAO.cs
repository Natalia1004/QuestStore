using System;
using QuestStoreNAT.web.Models;
using System.Collections.Generic;
using Npgsql;

namespace QuestStoreNAT.web.Services
{
    public class StudentDAO : IStudentDAO
    {
        public List<Student> GetAllRows()
        {
            List<Student> dataStudents = new List<Student>();

            using NpgsqlConnection connection = ConnectDB.CreateNewConnection();
            connection.Open();

            string sql = $"SELECT * FROM students";
            using NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            using NpgsqlDataReader reader = command.ExecuteReader();

            int countOfData = reader.FieldCount;

            while (reader.Read())
            {
                Student student = new Student() {
                    StudentID = Convert.ToInt16(reader[0]),
                    ClassID = Convert.ToInt16(reader[1]),
                    GroupID = Convert.ToInt16(reader[2]),
                    Email = Convert.ToString(reader[3]),
                    Password = Convert.ToString(reader[4]),
                    FirstName = Convert.ToString(reader[5]),
                    LastName = Convert.ToString(reader[6]),
                    CC_Wallet = Convert.ToInt16(reader[7])
                };
                dataStudents.Add(student);
            }
            return dataStudents;
        }
    }
}
