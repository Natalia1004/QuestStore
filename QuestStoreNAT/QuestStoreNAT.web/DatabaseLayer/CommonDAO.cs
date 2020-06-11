using System;
using System.Collections.Generic;
using Npgsql;

namespace QuestStoreNAT.web.DatabaseLayer
{
    public class CommonDAO : ICommonDAO
    {
        public void DeleteRow(string tableName, int id)
        {
            string sql = $"DELETE FROM {tableName} WHERE id = {id}";
            ConnectDB.ExecuteNonQuery(sql);
        }
    }
}