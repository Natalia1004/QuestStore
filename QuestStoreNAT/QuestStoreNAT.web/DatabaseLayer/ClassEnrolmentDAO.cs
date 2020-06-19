using System;
using QuestStoreNAT.web.Models;
using Npgsql;
using Npgsql.PostgresTypes;
using System.Collections.Generic;

namespace QuestStoreNAT.web.DatabaseLayer
{
    public class ClassEnrolmentDAO
    {
        public string DBTableName { get; set; } = "ClassEnrollment";


        public List<ClassEnrollment> FetchAllRecords()
        {
            using NpgsqlConnection connection = OpenConnectionToDB();
            var query = ProvideQueryFULL();
            using var command = new NpgsqlCommand(query , connection);
            var reader = command.ExecuteReader();

            var allRecords = new List<ClassEnrollment>();
            while ( reader.Read() )
            {
                allRecords.Add(ProvideOneRecord(reader));
            };
            return allRecords;
        }

        private ClassEnrollment ProvideOneRecord( NpgsqlDataReader reader )
        {
            var classEnrol = new ClassEnrollment
            {
                Id = reader.GetInt32(0) ,
                MentorID = reader.GetInt32(1) ,
                MentorCE =
                {
                    FirstName = reader.GetString(4),
                    LastName = reader.GetString(5)
                } ,
                ClassroomId = reader.GetInt32(2) ,
                ClassroomCE =
                {
                    Name = reader.GetString(3)
                }
            };

            return classEnrol;
        }

        private NpgsqlConnection OpenConnectionToDB()
        {
            var connection = new NpgsqlConnection(ConnectDB.GetConnectionString());
            connection.Open();
            return connection;
        }

        private void ExecuteQuery( NpgsqlConnection connection , string query )
        {
            using var command = new NpgsqlCommand(query , connection);
            command.Prepare();
            command.ExecuteNonQuery();
        }

        private void ExecuteReader( NpgsqlConnection connection , string query )
        {
            using var command = new NpgsqlCommand(query , connection);
            command.Prepare();
            command.ExecuteNonQuery();

        }

        private string ProvideQueryFULL()
        {
            string query = "SELECT CE.\"ID\", ME.\"ID\", ME.\"FirstName\", ME.\"Surname\", CS.\"ID\", CS.\"Name\"" +
                "FROM \"NATQuest\".\"ClassEnrollment\" CE " +
                "FULL JOIN \"NATQuest\".\"Mentors\" ME on CE.\"MentorID\" = ME.\"ID\"" +
                "FULL JOIN \"NATQuest\".\"Classes\" CS on CE.\"ClassID\" = CS.\"ID\""+
                "order by ME.\"ID\";";
            return query;
        }
    }
}
