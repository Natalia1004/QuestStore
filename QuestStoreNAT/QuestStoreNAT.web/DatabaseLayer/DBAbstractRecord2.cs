using Npgsql;
using System;
using System.Collections.Generic;

namespace QuestStoreNAT.web.DatabaseLayer
{
    public abstract class DBAbstractRecord2<T>
    {
        public abstract string DBTableName { get; set; }
        //public abstract T ProvideOneRecord2( NpgsqlDataReader reader );
        public abstract string ProvideQueryStringToAdd( T recordToAdd );


        public virtual int AddRecordReturningID( T recordToAdd )
        {
            using NpgsqlConnection connection = OpenConnectionToDB();
            string query = ProvideQueryStringToAdd(recordToAdd);
            return ExecuteScalar(connection , query);
        }


        protected NpgsqlConnection OpenConnectionToDB()
        {
            var connection = new NpgsqlConnection(ConnectDB.GetConnectionString());
            connection.Open();
            return connection;
        }

        protected int ExecuteScalar( NpgsqlConnection connection , string query )
        {
            using var command = new NpgsqlCommand(query , connection);
            command.Prepare();
            return Convert.ToInt32(command.ExecuteScalar());
        }
    }
}
